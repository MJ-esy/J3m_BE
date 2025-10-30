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

        //Returns all recipes that are linked to a specific diet - projected to DTOs
        public async Task<IEnumerable<Recipe>> GetRecipesByDietAsync(int id)
        {
            return await _context.DietRecipes
                .AsNoTracking()
                .Where(dr => dr.DietId == id)
                .Include(dr => dr.Recipe)
                 .Select(dr => new Recipe
                 {
                     RecipeId = dr.Recipe.RecipeId,
                     RecipeName = dr.Recipe.RecipeName
                 })
                .ToListAsync();
        }


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




//21 + 38 Include(d => d.RecipeLinks) - Loads full recipeLinks might be overkill?
//37.Include(dr => dr.Recipe) - are we accessing other properties from dr?
// 17 & 23. När RecipeDto finns 
         //.Select(dr => new RecipeDto
         // {
         //     RecipeId = dr.RecipeId,
         //     RecipeName = dr.Recipe.RecipeName
         //     //... add more fields if as needed
         // })

