using J3m_BE.Data;
using J3m_BE.DTOs.Ingredients;
using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Repositories.Implementations;

// Ingredient Repository Implementation

public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(AppDbContext context) : base(context) { }
    
    // Get one Ingredient with all related info
    public async Task<Ingredient?> GetWithDetailsAsync(int id)
    {
        return await _context.Ingredients
            .Include(i => i.FoodGroup)
            .Include(i => i.AllergyLinks).ThenInclude(ia => ia.Allergy)
            .Include(i => i.NutrientLinks).ThenInclude(ing => ing.NutrientGroup)
            .FirstOrDefaultAsync(i => i.IngredientId == id);
    }
    
    // Check if used in any Recipe (to prevent deletion if in use)
    public async Task<bool> IsUsedInRecipeAsync(int ingredientId)
    => await _context.IngredientRecipes.AnyAsync(ir => ir.IngredientId == ingredientId);
}
