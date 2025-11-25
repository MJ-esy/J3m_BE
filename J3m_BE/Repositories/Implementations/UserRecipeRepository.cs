using J3m_BE.Data;
using J3m_BE.Models.Links;
using J3m_BE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Repositories.Implementations
{
    public class UserRecipeRepository : IUserRecipeRepository
    {
        private readonly AppDbContext _context;

        public UserRecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get a specific UserRecipe by userId and recipeId
        public async Task<UserRecipe?> GetUserRecipeAsync(string userId, int recipeId)
        {
            return await _context.UserRecipes
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RecipeId == recipeId);
        }

        // Add a new UserRecipe
        public async Task AddUserRecipeAsync(UserRecipe userRecipe)
        {
            _context.UserRecipes.Add(userRecipe);
            await _context.SaveChangesAsync();
        }

        // Update an existing UserRecipe
        public async Task UpdateUserRecipeAsync(UserRecipe userRecipe)
        {
            _context.UserRecipes.Update(userRecipe);
            await _context.SaveChangesAsync();
        }

        // Get all favorite recipes for a user
        public async Task<IEnumerable<UserRecipe>> GetFavoritesByUserAsync(string userId)
        {
            return await _context.UserRecipes
                .Where(ur => ur.UserId == userId && ur.IsFavorite)
                .Include(ur => ur.Recipe)
                .ToListAsync();
        }
    }
}
