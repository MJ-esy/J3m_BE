using J3m_BE.DTOs.Allergies;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;

namespace J3m_BE.Services.Implementations
{
    public class AllergyService : IAllergyService
    {
        private readonly IAllergyRepository _allergyRepository;

        public AllergyService(IAllergyRepository allergyRepository)
        {
            _allergyRepository = allergyRepository;
        }

        public async Task<List<AllergyDto>> GetAllAllergiesAsync()
        {
            return await _allergyRepository.GetAllAllergiesAsync();
        }

        public async Task<AllergyDto?> GetAllergyByIdAsync(int allergyId)
        {
            return await _allergyRepository.GetAllergyByIdAsync(allergyId);
        }

        public async Task<AllergyCreateDto?> CreateAllergyAsync(AllergyCreateDto allergyCreateDto)
        {
            return await _allergyRepository.CreateAllergyAsync(allergyCreateDto);
        }
        public async Task<AllergyUpdateDto?> UpdateAllergyAsync(int allergyId, AllergyUpdateDto allergyUpdateDto)
        {
           return await _allergyRepository.UpdateAllergyAsync(allergyId, allergyUpdateDto);
        }
        public async Task<bool> DeleteAllergyAsync(int allergyId)
        {
            return await _allergyRepository.DeleteAllergyAsync(allergyId);
        }
    }
}
