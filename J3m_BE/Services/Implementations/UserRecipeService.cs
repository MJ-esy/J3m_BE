using J3M.Shared.DTOs.UserRecipes;
using J3m_BE.Exceptions;
using J3m_BE.Mappers;
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

        // Get a specific UserRecipe
        public async Task<UserRecipeDto?> GetUserRecipeAsync(string userId, int recipeId)
        {
            var entity = await _repository.GetUserRecipeAsync(userId, recipeId);
            return entity?.ToDto();
        }

        // Get all favorite recipes for a user
        public async Task<IEnumerable<UserRecipeDto>> GetFavoriteRecipesAsync(string userId)
        {
            var favorites = await _repository.GetFavoritesByUserAsync(userId);
            if (favorites == null)
                throw new NotFoundDomainException("No saved recipes available");
            return favorites.Select(f => f.ToDto());
        }

        // Mark a recipe as favorite for a user
        public async Task<bool> FavoriteRecipeAsync(string userId, int recipeId)
        {
            var existing = await _repository.GetUserRecipeAsync(userId, recipeId);
            if (existing == null)
            {
                await _repository.AddUserRecipeAsync(new UserRecipe
                {
                    UserId = userId,
                    RecipeId = recipeId,
                    IsFavorite = true
                });
            }
            else
            {
                existing.IsFavorite = true;
                await _repository.UpdateUserRecipeAsync(existing);
            }
            return true;
        }

        // Unmark a recipe as favorite for a user
        public async Task<bool> UnfavoriteRecipeAsync(string userId, int recipeId)
        {
            var existing = await _repository.GetUserRecipeAsync(userId, recipeId);
            if (existing == null) return false;

            existing.IsFavorite = false;
            await _repository.UpdateUserRecipeAsync(existing);
            return true;
        }
    }
}
