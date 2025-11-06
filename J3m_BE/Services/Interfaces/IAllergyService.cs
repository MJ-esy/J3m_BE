using J3m_BE.DTOs.Allergies;

namespace J3m_BE.Services.Interfaces
{
    public interface IAllergyService
    {
        Task<IEnumerable<AllergyDto>> GetAllAsync();
        Task<AllergyDto?> GetByIdAsync(int Id);
        Task<int> CreateAsync(AllergyCreateDto dto);
        Task<bool> UpdateAsync(int Id, AllergyUpdateDto dto);
        Task<bool> DeleteAsync(int Id);

    }
}
