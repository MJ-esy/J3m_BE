using J3m_BE.Models;

namespace J3m_BE.Repositories.Interfaces;

// Interface for Recipe repository extending generic repository
// Defines methods for fetching recipes with related diets and ingredients

public interface IRecipeRepository : IGenericRepository<Recipe>
{
    Task<Recipe?> GetWithDetailsAsync(int id);
    IQueryable<Recipe> QueryWithDietsAndIngredients(); // New method for querying with related data
}