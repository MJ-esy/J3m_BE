using J3m_BE.Models;

namespace J3m_BE.Repositories.Interfaces;

// Interface for Recipe repository extending generic repository
// Defines methods for fetching recipes with related diets and ingredients

public interface IRecipeRepository : IGenericRepository<Recipe>
{
    // Get a recipe by ID including related diets and ingredients
    Task<Recipe?> GetWithDetailsAsync(int id);
    
    // Queryable for recipes including related diets and ingredients
    IQueryable<Recipe> QueryWithIncludes();
    
    // Check if a recipe exists by name
    Task<bool> ExistsByNameAsync(string name);

    //Filter recipes with more than 3 ingredients linked to recipe
    Task<List<Recipe>> GetByMatchingIngredientsAsync(IEnumerable<int> ingredientIds, int minMatchCount);
}