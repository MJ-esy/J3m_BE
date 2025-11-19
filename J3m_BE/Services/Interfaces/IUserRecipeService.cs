using J3m_BE.Models;

namespace J3m_BE.Services.Interfaces
{
    public interface IUserRecipeService
    {
        Task<bool> FavoriteRecipeAsync(string userId, int recipeId);
        Task<bool> UnfavoriteRecipeAsync(string userId, int recipeId);
        Task<IEnumerable<Recipe>> GetFavoriteRecipesAsync(string userId);
    }

}
