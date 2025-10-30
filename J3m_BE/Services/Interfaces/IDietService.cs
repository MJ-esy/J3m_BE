using J3m_BE.DTOs.Diets;
using J3m_BE.Models;

namespace J3m_BE.Services.Interfaces
{
    public interface IDietService
    {
        Task<IEnumerable<DietDto>> GetAllAsync();
        Task<DietDto?> GetByIdAsync(int id);
        Task<DietDto?> CreateAsync(CreateDietDto dto);
        Task<bool> UpdateAsync(int id,UpdateDietDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<DietWithCountDto>> GetDietsWithRecipeCountAsync();
        Task<IEnumerable<Recipe>> GetRecipesByDietAsync(int id);
    }
    //14. Byt till  <IEnumerable<RecipeListItemDto>> 
}
