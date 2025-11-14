using J3m_BE.DTOs.Diets;
using J3m_BE.Models;

namespace J3m_BE.Services.Interfaces
{
    public interface IDietService
    {
        Task<IEnumerable<DietWithCountDto>> GetAllAsync();

        Task<DietDto?> GetByIdAsync(int id);
        Task<DietDto?> CreateAsync(DietCreateDto dto);
        Task<bool> UpdateAsync(int id,DietUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        
    }
}
