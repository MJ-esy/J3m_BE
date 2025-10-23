using J3m_BE.Data;
using J3m_BE.Models;
using J3m_BE.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Repositories;

public class RecipeRepository : GenericRepository<Recipe>
{
    public RecipeRepository (AppDbContext context) : base(context) { }
    
    public async Task<Recipe?> GetWithDetailsAsync(int id)
    {
        return await _context.Recipes
            .Include(r => r.IngredientLinks)
                .ThenInclude(ir => ir.Ingredient)
            .Include(r => r.DietLinks)
                .ThenInclude(dr => dr.Diet)
            .FirstOrDefaultAsync(r => r.RecipeId == id);
    }
}