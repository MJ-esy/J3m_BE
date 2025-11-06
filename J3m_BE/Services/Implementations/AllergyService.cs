using J3m_BE.DTOs.Allergies;
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

        // Create a new allergy
        public async Task<int> CreateAsync(AllergyCreateDto dto)
        {
            var name = dto.AllergyName?.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Allergy name is required.");
           
            // Check for duplicate names
            if (await _allergyRepository.ExistsAsync(a => a.AllergyName.ToLower() == name.ToLower()))
                throw new ConflictDomainException($"Allergy '{name}' already exists.");
          
            var entity = dto.ToEntity();
            await _allergyRepository.AddAsync(entity);
            await _allergyRepository.SaveChangesAsync();
            return entity.AllergyId;
        }

        // Update an existing allergy
        public async Task<bool> UpdateAsync(int id, AllergyUpdateDto dto)
        {
            var entity = await _allergyRepository.GetByIdAsync(id);
            if (entity is null)
                throw new NotFoundDomainException($"Allergy with ID {id} not found.");
           
            dto.MapToEntity(entity);
            _allergyRepository.Update(entity);
            await _allergyRepository.SaveChangesAsync();
            return true;
        }

        // Delete an allergy
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _allergyRepository.GetByIdAsync(id);
            if (entity is null)
                throw new NotFoundDomainException($"Allergy with ID {id} not found.");
           
            _allergyRepository.Remove(entity);
            await _allergyRepository.SaveChangesAsync();
            return true;
        }

    }
}
