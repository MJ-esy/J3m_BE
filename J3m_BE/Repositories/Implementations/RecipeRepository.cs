using J3m_BE.Data;
using J3m_BE.Models;
using J3m_BE.Repositories.Implementations;
using J3m_BE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Repositories;

// Repository for managing Recipe entities with related data
// Inherits from GenericRepository and implements IRecipeRepository

public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
{
    public RecipeRepository(AppDbContext context) : base(context)
    {
    }
    
    // Fetch a single Recipe with related data
    public async Task<Recipe?> GetWithDetailsAsync(int id)
    {
        return await _context.Recipes
            .Include(r => r.IngredientLinks).ThenInclude(ir => ir.Ingredient)
            .Include(r => r.DietLinks).ThenInclude(dr => dr.Diet)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RecipeId == id);
    }
    
    // Base query for projection/filtering with related data
    public IQueryable<Recipe> QueryWithIncludes()
    {
        return _context.Recipes
            .Include(r => r.IngredientLinks).ThenInclude(ir => ir.Ingredient)
            .Include(r => r.DietLinks).ThenInclude(dr => dr.Diet)
            .AsNoTracking();
    }
    
    // Check if a Recipe exists by name
    public async Task<bool> ExistsByNameAsync(string name)
    => await _context.Recipes.AnyAsync(r => r.RecipeName.ToLower() == name.ToLower());

}