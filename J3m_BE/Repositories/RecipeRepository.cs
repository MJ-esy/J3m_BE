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
    public RecipeRepository (AppDbContext context) : base(context) { }
    
    // Fetch a recipe by ID including its diets and ingredients
    public Task<Recipe?> GetWithDetailsAsync(int id) =>
    _context.Recipes
        .AsNoTracking()
        .Include(r => r.DietLinks).ThenInclude(dr => dr.Diet)
        .Include(r => r.IngredientLinks).ThenInclude(ir => ir.Ingredient)
        .FirstOrDefaultAsync(r => r.RecipeId == id);

    // Provide a queryable for recipes including diets and ingredients
    public IQueryable<Recipe> QueryWithDietsAndIngredients() =>
        _context.Recipes
            .AsNoTracking()
            .Include(r => r.DietLinks).ThenInclude(dr => dr.Diet)
            .Include(r => r.IngredientLinks).ThenInclude(ir => ir.Ingredient);
}