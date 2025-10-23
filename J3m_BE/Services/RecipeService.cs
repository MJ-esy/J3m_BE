using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;

namespace J3m_BE.Services;

public class RecipeService : IRecipeService
{
    private readonly IGenericRepository<Recipe> _repo;
    
    public RecipeService(IGenericRepository<Recipe> repo)
    {
        _repo = repo;
    }
    
    public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
    {
        return await _repo.GetAllAsync();
    }
    
    public async Task<Recipe?> GetRecipeByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }
    
    public async Task<Recipe> CreateAsync(Recipe recipe)
    {
        await _repo.AddAsync(recipe);
        return recipe;
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        var recipe = await _repo.GetByIdAsync(id);
        if (recipe == null) return false;
        
        _repo.Delete(recipe);
        await _repo.SaveChangesAsync();
        return true;
    }
}