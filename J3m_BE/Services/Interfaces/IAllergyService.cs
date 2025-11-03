using J3m_BE.DTOs.Allergies;

namespace J3m_BE.Services.Interfaces
{
    public interface IAllergyService
    {
        Task<List<AllergyDto>> GetAllAllergiesAsync();
        Task<AllergyDto?> GetAllergyByIdAsync(int allergyId);
        Task<AllergyCreateDto?> CreateAllergyAsync(AllergyCreateDto allergyCreateDto);
        Task<AllergyUpdateDto?> UpdateAllergyAsync(int allergyId, AllergyUpdateDto allergyUpdateDto);
        Task<bool> DeleteAllergyAsync(int allergyId);

    }
}
