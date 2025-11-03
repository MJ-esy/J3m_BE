using J3m_BE.DTOs.Allergies;

namespace J3m_BE.Repositories.Interfaces
{
  
    public interface IAllergyRepository
    {
        Task<List<AllergyDto>> GetAllAllergiesAsync();
        Task<AllergyDto?> GetAllergyByIdAsync(int allergyId);
        Task<AllergyDto> CreateAllergyAsync(AllergyCreateDto allergyCreateDto);
        Task<AllergyDto?> UpdateAllergyAsync(int allergyId, AllergyUpdateDto allergyUpdateDto);
        Task<bool> DeleteAllergyAsync(int allergyId);

    }
}
