using J3m_BE.DTOs.Allergies;

namespace J3m_BE.Services.Interfaces
{
    public interface IAllergyService
    {
        Task<IEnumerable<AllergyWithCountDto>> GetAllAsync();
        Task<AllergyWithCountDto?> GetByIdAsync(int Id);
        Task<int> CreateAsync(AllergyCreateDto dto);
        Task<bool> UpdateAsync(int Id, AllergyUpdateDto dto);
        Task<bool> DeleteAsync(int Id);

    }
}
