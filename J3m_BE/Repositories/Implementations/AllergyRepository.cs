using J3m_BE.DTOs.Allergies;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Data;
using Microsoft.EntityFrameworkCore;
using J3m_BE.Models;

// use generic repo !
namespace J3m_BE.Repositories.Implementations
{
    public class AllergyRepository : GenericRepository<Allergy>, IAllergyRepository
    {
        public AllergyRepository(AppDbContext context) : base(context) { }

        // Fetch a single Allergy with the associated ingredients
        // Fix GetWithIngredientsAsync to match interface return type
        public Task<Allergy?> GetWithIngredientsAsync(int id) =>
             _context.Allergies
                .AsNoTracking()
                .Include(a => a.IngredientLinks)
                .FirstOrDefaultAsync(a => a.AllergyId == id);


        // Fix GetAllAllergiesWithCountAsync to match interface return type
        public async Task<IEnumerable<AllergyWithCountDto>> GetAllAllergiesWithCountAsync()
        {
            return await _context.Allergies
                .AsNoTracking()
                .Select(a => new AllergyWithCountDto
                {
                    AllergyId = a.AllergyId,
                    AllergyName = a.AllergyName,
                    IngredientCount = a.IngredientLinks.Count()
                }).ToListAsync();
        }
    }
}

