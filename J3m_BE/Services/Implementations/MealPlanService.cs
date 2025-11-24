using J3M.Shared.MealPlanModels;
using J3m_BE.Exceptions;
using J3m_BE.Extensions;
using J3m_BE.Mappers;
using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Services.Implementations
{
    public class MealPlanService : IMealPlanService
    {
        private readonly IRecipeRepository _repo;
        private readonly IAzureOpenAiService _ai;
        public MealPlanService(IRecipeRepository repo, IAzureOpenAiService ai)
        {
            _repo = repo;
            _ai = ai;
        }

        // Filter Recipes by allergies and diets IDs
        public async Task<List<DayMealPlanDto>> FilterRecipeAsync(List<int>? allergyIds, List<int>? dietIds)
        {
            // Clean up lists if id == 0
            var cleanedAllergyIds = allergyIds.NormalizeIds();
            var cleanedDietIds = dietIds.NormalizeIds();

            // If both lists are null or empty, return all recipes; otherwise apply filters.
            List<Recipe> recipeList;
            var noAllergy = cleanedAllergyIds.Count == 0;
            var noDiet = cleanedDietIds.Count == 0;

            if (noAllergy && noDiet)
                // return all recipes with related navigation properties
                recipeList = await _repo.QueryWithIncludes().ToListAsync();
            else
                // repository method ignores empty lists, but ensure non-null arguments
                recipeList = await _repo.GetWithAllergyDietFilterAsync(cleanedAllergyIds, cleanedDietIds);

            if (recipeList is null || !recipeList.Any())
                throw new NotFoundDomainException("No recipes found matching the provided allergies and diets.");

            //Randomize selection
            var random = new Random();

            // Initialize meal plan and used recipe IDs
            var weeklyPlan = new List<DayMealPlanDto>();
            var usedRecipeIds = new HashSet<int>();

            for (int day = 0; day < 7; day++)
            {
                // Create a new meal plan for the day
                var dayMeals = new List<MealSlotDto>();

                // Pick Lunch Recipe
                var lunchRecipe = PickRecipe(recipeList, usedRecipeIds, random);
                dayMeals.Add(new MealSlotDto { MealType = "Lunch", Recipe = lunchRecipe.ToDetailDto() });

                // Pick Dinner Recipe
                var dinnerRecipe = PickRecipe(recipeList, usedRecipeIds, random, lunchRecipe.RecipeId);
                dayMeals.Add(new MealSlotDto { MealType = "Dinner", Recipe = dinnerRecipe.ToDetailDto() });

                weeklyPlan.Add(new DayMealPlanDto
                {
                    Day = ((DayOfWeek)day).ToString(),
                    Meals = dayMeals
                });

            }
            return weeklyPlan;
        }

        // Pick a recipe that is not in the usedRecipeIds and not the excluded one
        private Recipe PickRecipe(List<Recipe> recipes, HashSet<int> usedRecipeIds, Random random, int? excludeId = null)
        {
            // Catch exception if recipeList is empty
            if (recipes == null || recipes.Count == 0)
                throw new NotFoundDomainException("Recipe list is empty. List must contain at least one element");

            // Initiate a recipe from the filtered Recipe List and max attempts
            const int maxAttempts = 10;
            Recipe recipe = recipes[0];
            int attempts = 0;

            // Try random picks up to max attempts
            while (attempts < maxAttempts)
            {
                recipe = recipes[random.Next(recipes.Count)];
                attempts++;

                // check if the picked recipe is not excluded and not used
                if ((!excludeId.HasValue || recipe.RecipeId != excludeId.Value) && !usedRecipeIds.Contains(recipe.RecipeId))
                    break;
            }

            // Deterministic fallback if random attempts failed
            // (Condition: Check if the picked recipe is excluded or already used)
            if ((excludeId.HasValue && recipe.RecipeId == excludeId.Value) || usedRecipeIds.Contains(recipe.RecipeId))
            {
                //Condition: Make sure that there is at least one available recipe to pick from the excluded list
                var fallback = recipes.FirstOrDefault(r => (!excludeId.HasValue || r.RecipeId != excludeId.Value)
                                                                && !usedRecipeIds.Contains(r.RecipeId));
                // If a fallback is found, use it. Otherwise use the recipe picked last
                recipe = fallback ?? recipe;
            }

            usedRecipeIds.Add(recipe.RecipeId);
            return recipe;
        }

        // Create Weekly Meal Plan with AI enrichment
        public async Task<WeeklyMealPlanDto> CreateWeeklyMealPlanWithAiAsync(List<int> allergyIds, List<int> dietIds)
        {
            // First, filter recipes based on allergies and diets
            var filteredPlan = await FilterRecipeAsync(allergyIds, dietIds);

            // use AI to enrich the meal plan with a shopping list and summary
            var aiResponse = await _ai.EnrichAsync(filteredPlan, allergyIds, dietIds);

            // Merge AI summaries per day
            for (int i = 0; i < filteredPlan.Count && i < aiResponse.DaySummaries.Count; i++)
            {
                filteredPlan[i].Summary = aiResponse.DaySummaries[i].Summary;
            }

            // Return the final weekly meal plan with shopping list
            return new WeeklyMealPlanDto
            {
                DaySummaries = aiResponse.DaySummaries,
                ShoppingList = aiResponse.ShoppingList
            };

        }
    }
}

