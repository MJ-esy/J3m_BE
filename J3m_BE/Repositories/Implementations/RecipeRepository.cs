using J3m_BE.Data;
using J3m_BE.Models;
using J3m_BE.Repositories.Implementations;
using J3m_BE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Repositories;

// Repository for managing Recipe entities with related data
// Inherits from GenericRepository and implements IRecipeRepository

public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
{
    private readonly HttpClient _client;
    public RecipeRepository(AppDbContext context) : base(context)
    {
    }

    // Fetch a single Recipe with related data
    public async Task<Recipe?> GetWithDetailsAsync(int id)
    {
        return await _context.Recipes
            .Include(r => r.IngredientLinks).ThenInclude(ir => ir.Ingredient)
            .Include(r => r.DietLinks).ThenInclude(dr => dr.Diet)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RecipeId == id);
    }

    // Base query for projection/filtering with related data
    public IQueryable<Recipe> QueryWithIncludes()
    {
        return _context.Recipes
            .Include(r => r.IngredientLinks).ThenInclude(ir => ir.Ingredient)
            .Include(r => r.DietLinks).ThenInclude(dr => dr.Diet)
            .AsNoTracking();
    }

    // Check if a Recipe exists by name
    public async Task<bool> ExistsByNameAsync(string name)
    => await _context.Recipes.AnyAsync(r => r.RecipeName.ToLower() == name.ToLower());

    // Filter recipes with more than minMatchCount ingredients linked to recipe
    public async Task<List<Recipe>> GetByMatchingIngredientsAsync(IEnumerable<int> ingredientIds, int minMatchCount)
    {
        var ingredientIdList = ingredientIds.ToList();

        //Return empty list if no ingredients provided or minMatchCount is zero or negative
        if (ingredientIdList.Count == 0 || minMatchCount <= 0)
            return new List<Recipe>();

        return await _context.Recipes
            .Include(r => r.IngredientLinks).ThenInclude(ir => ir.Ingredient)
            .Include(r => r.DietLinks).ThenInclude(dr => dr.Diet)
            .AsNoTracking()
            .Where(r => r.IngredientLinks.Count(ri => ingredientIdList.Contains(ri.IngredientId)) >= minMatchCount)
            .Select(r => new
            {
                Recipe = r,
                MatchCount = r.IngredientLinks.Count(ri => ingredientIdList.Contains(ri.IngredientId))
            })
            .OrderByDescending(x => x.MatchCount)
            .Select(x => x.Recipe)
            .ToListAsync();
    }

    //Filter recipes by allergies and diets
    public async Task<List<Recipe>> GetWithAllergyDietFilterAsync(List<int> allergyIds, List<int> dietIds)
    {
        var recipeList = QueryWithIncludes();

        if (allergyIds != null && allergyIds.Count > 0)
        {
            recipeList = recipeList.Where(r => !r.IngredientLinks
                .Any(ir => ir.Ingredient.AllergyLinks
                .Any(ia => allergyIds.Contains(ia.AllergyId))));
        }

        if (dietIds != null && dietIds.Count > 0)
        {
            recipeList = recipeList.Where(r => r.DietLinks
                .Any(dr => dietIds.Contains(dr.DietId)));
        }
        else if (allergyIds.Count == 0 && dietIds.Count == 0)
        {
            return await recipeList.ToListAsync();
        }

        // If both allergyIds and dietIds are empty, return all recipes
        return await recipeList.ToListAsync();
    }
}