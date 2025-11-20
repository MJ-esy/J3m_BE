using J3M.Shared.DTOs.UserRecipes;
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

        public async Task<UserRecipeDto?> GetUserRecipeAsync(string userId, int recipeId)
        {
            var entity = await _repository.GetUserRecipeAsync(userId, recipeId);
            return entity?.ToDto();
        }

        public async Task AddUserRecipeAsync(UserRecipeDto userRecipeDto)
        {
            var entity = new UserRecipe
            {
                UserId = userRecipeDto.UserId,
                RecipeId = userRecipeDto.RecipeId,
                IsFavorite = userRecipeDto.IsFavorite
            };
            await _repository.AddUserRecipeAsync(entity);
        }

        public async Task UpdateUserRecipeAsync(UserRecipeDto userRecipeDto)
        {
            var entity = new UserRecipe
            {
                UserId = userRecipeDto.UserId,
                RecipeId = userRecipeDto.RecipeId,
                IsFavorite = userRecipeDto.IsFavorite
            };
            await _repository.UpdateUserRecipeAsync(entity);
        }

        public async Task<IEnumerable<UserRecipeDto>> GetFavoriteRecipesAsync(string userId)
        {
            return await _repository.GetFavoritesByUserAsync(userId);
        }

        // New methods
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
