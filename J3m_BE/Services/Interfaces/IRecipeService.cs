using J3m_BE.Models;

namespace J3m_BE.Services.Interfaces;

public interface IRecipeService
{
    Task<IEnumerable<Recipe>> GetAllRecipesAsync();
    Task<Recipe?> GetRecipeByIdAsync(int id);
    Task<Recipe> CreateAsync(Recipe recipe);
    Task<bool> DeleteAsync(int id);
}