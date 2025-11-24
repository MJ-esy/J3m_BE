using J3M.Shared.DTOs.UserRecipes;
using J3m_BE.DTOs.Recipes;
using J3m_BE.Models;

namespace J3m_BE.Services.Interfaces
{
    public interface IUserRecipeService
    {
        Task<UserRecipeDto?> GetUserRecipeAsync(string userId, int recipeId);
        Task<IEnumerable<UserRecipeDto>> GetFavoriteRecipesAsync(string userId);

        Task<bool> FavoriteRecipeAsync(string userId, int recipeId);
        Task<bool> UnfavoriteRecipeAsync(string userId, int recipeId);

    }

}
