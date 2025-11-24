using J3M.Shared.DTOs.NutrientGroups;
using J3m_BE.Data;
using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Repositories.Implementations
{
    // Repository for managing NutrientGroup entities
    // Implements methods defined in INutrientGroupRepository, for future-proofing data access
    public class NutrientGroupRepository : GenericRepository<NutrientGroup>, INutrientGroupRepository
    {
        public NutrientGroupRepository(AppDbContext context) : base(context) { }

        //Fetch a NutrientGroup by ID including related Ingredients
        public async Task<NutrientGroup?> GetWithDetailsAsync(int id) =>
               await _context.NutrientGroups
                  .AsNoTracking()
                  .Include(n => n.IngredientLinks)
                    .ThenInclude(i => i.Ingredient)
                  .FirstOrDefaultAsync(n => n.NutrientGroupId == id);

        //Fetch all NutrientGroups with count of associated Ingredients
        public async Task<IEnumerable<NutrientGroupDto>> GetAllWithIngredientsCountAsync()
        {
            return await _context.NutrientGroups
              .AsNoTracking()
              .Select(n => new NutrientGroupDto
              {
                  NutrientGroupId = n.NutrientGroupId,
                  NutrientGroupName = n.NutrientGroupName,
                  IngredientCount = n.IngredientLinks.Count()
              })
              .ToListAsync();
        }
    }
}
