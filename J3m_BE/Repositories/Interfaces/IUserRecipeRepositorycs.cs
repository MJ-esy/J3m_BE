using J3M.Shared.DTOs.UserRecipes;
using J3m_BE.Models.Links;

namespace J3m_BE.Repositories.Interfaces
{
 
        public interface IUserRecipeRepository
        {
            Task<UserRecipe?> GetUserRecipeAsync(string userId, int recipeId);
            Task AddUserRecipeAsync(UserRecipe userRecipe);
            Task UpdateUserRecipeAsync(UserRecipe userRecipe);
            Task<IEnumerable<UserRecipeDto>> GetFavoritesByUserAsync(string userId);
        }
    }



