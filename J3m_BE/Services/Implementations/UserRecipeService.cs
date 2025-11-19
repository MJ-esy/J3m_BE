using J3m_BE.Models;
using J3m_BE.Models.Links;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;

namespace J3m_BE.Services.Implementations
{
    public class UserRecipeService : IUserRecipeService
    {
        private readonly IUserRecipeRepository _repository;

        public UserRecipeService(IUserRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> FavoriteRecipeAsync(string userId, int recipeId)
        {
            var userRecipe = await _repository.GetUserRecipeAsync(userId, recipeId);

            if (userRecipe == null)
            {
                userRecipe = new UserRecipe
                {
                    UserId = userId,
                    RecipeId = recipeId,
                    IsFavorite = true
                };
                await _repository.AddUserRecipeAsync(userRecipe);
            }
            else
            {
                userRecipe.IsFavorite = true;
                await _repository.UpdateUserRecipeAsync(userRecipe);
            }

            return true;
        }

        public async Task<bool> UnfavoriteRecipeAsync(string userId, int recipeId)
        {
            var userRecipe = await _repository.GetUserRecipeAsync(userId, recipeId);
            if (userRecipe == null) return false;

            userRecipe.IsFavorite = false;
            await _repository.UpdateUserRecipeAsync(userRecipe);
            return true;
        }

        public async Task<IEnumerable<Recipe>> GetFavoriteRecipesAsync(string userId)
        {
            var favorites = await _repository.GetFavoritesByUserAsync(userId);
            return favorites.Select(f => f.Recipe);
        }
    }

}
