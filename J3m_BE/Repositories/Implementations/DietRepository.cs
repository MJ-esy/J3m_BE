using J3m_BE.Models;
using J3m_BE.DTOs.Diets;
using J3m_BE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using J3m_BE.Data;
using J3m_BE.Mappers;



namespace J3m_BE.Repositories.Implementations
{
    public class DietRepository : GenericRepository<Diet>, IDietRepository
    {
        public DietRepository(AppDbContext context) : base(context) { }

        //Return a single diet along with its linked recipes
        public async Task<Diet?> GetWithDetailsAsync(int id) =>
        
            await _context.Diets
                .AsNoTracking()
                .Include(d => d.RecipeLinks)
                .ThenInclude(r => r.Recipe)
                .FirstOrDefaultAsync(d => d.DietId == id);
        


        //Returns a list of all diets along with the number of recipes linked to each diet
        //Efficient projection: return DTOs directly
        public async Task<IEnumerable<DietWithCountDto>> GetDietWithRecipeCountAsync()
        {
            return await _context.Diets
                .AsNoTracking()
                 .Select(d => new DietWithCountDto
                 {
                     DietId = d.DietId,
                     DietName = d.DietName,
                     RecipeCount = d.RecipeLinks.Count()
                 })
                .ToListAsync();
        }
    }
}


