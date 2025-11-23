using J3M.Shared.MealPlanModels;
using J3m_BE.Exceptions;
using J3m_BE.Mappers;
using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;

namespace J3m_BE.Services.Implementations
{
    public class MealPlanService : IMealPlanService
    {
        private readonly IRecipeRepository _repo;
        public MealPlanService(IRecipeRepository repo)
        {
            _repo = repo;
        }

        // Filter Recipes by allergies and diets IDs
        public async Task<List<DayMealPlanDto>> FilterRecipeAsync(List<int> allergyIds, List<int> dietIds)
        {
            // Fetch recipes matching the allergy and diet filters
            var recipeList = await _repo.GetWithAllergyDietFilterAsync(allergyIds, dietIds);
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
    }
}

