using J3m_BE.Models;

namespace J3m_BE.Repositories.Interfaces;

// Ingredient repository interface extending generic repository

public interface IIngredientRepository : IGenericRepository<Ingredient>
{
    // Get one Ingredient with all related info
    Task<Ingredient?> GetWithDetailsAsync(int id);

    // Check if used in any Recipe (to prevent deletion if in use)
    Task<bool> IsUsedInRecipeAsync(int ingredientId);
}