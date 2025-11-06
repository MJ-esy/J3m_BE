using J3m_BE.DTOs.Allergies;
using J3m_BE.Extensions;
using J3m_BE.Models;
using J3m_BE.Mappers;
using J3m_BE.Exceptions;
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

        // Get all allergies with ingredient counts
        public async Task<IEnumerable<AllergyDto>> GetAllAsync() =>
            await _allergyRepository.GetAllAllergiesWithCountAsync();

        // Get an allergy by ID with associated ingredients
        public async Task<AllergyDto?> GetByIdAsync(int id)
        {
            var allergy = await _allergyRepository.GetWithIngredientsAsync(id);
            if (allergy is null)
                throw new NotFoundDomainException($"Allergy with ID {id} not found.");
            return allergy.ToDto();
        }
    }
}
