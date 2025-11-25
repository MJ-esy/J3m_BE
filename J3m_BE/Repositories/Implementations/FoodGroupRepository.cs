using J3M.Shared.DTOs.FoodGroups;
using J3m_BE.Data;
using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Repositories.Implementations;

// Repository for managing FoodGroup entities
// Implements methods defined in IFoodGroupRepository, for future-proofing data access

public class FoodGroupRepository : GenericRepository<FoodGroup>, IFoodGroupRepository
{
    public FoodGroupRepository(AppDbContext context) : base(context) { }

    // Fetch a single food group along with its associated ingredients
    public Task<FoodGroup?> GetWithIngredientsAsync(int id) =>
        _context.FoodGroups
            .AsNoTracking()
            .Include(f => f.Ingredients)
            .FirstOrDefaultAsync(f => f.FoodGroupId == id);

    // Fetch all FoodGroups with ingredient count (using efficient projection)
    public async Task<IEnumerable<FoodGroupDto>> GetAllWithIngredientsCountAsync()
    {
        return await _context.FoodGroups
            .AsNoTracking()
            .Select(f => new FoodGroupDto
            {
                FoodGroupId = f.FoodGroupId,
                FoodGroupName = f.FoodGroupName,
                IngredientCount = f.Ingredients.Count()
            })
            .ToListAsync();
    }
}