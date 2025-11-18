using J3m_BE.DTOs.NutrientGroups;
using J3m_BE.Exceptions;
using J3m_BE.Mappers;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;

namespace J3m_BE.Services
{
    // Service for managing Nutrient Group operations
    public class NutrientGroupService : INutrientGroupService
    {
        private readonly INutrientGroupRepository _repo;
        public NutrientGroupService(INutrientGroupRepository repo)
        {
            _repo = repo;
        }

        // Get all nutrient groups with ingredient counts
        public async Task<IEnumerable<NutrientGroupDto>> GetAllAsync() =>
            await _repo.GetAllWithIngredientsCountAsync();

        // Get a NutrientGroup by ID with associated ingredients
        public async Task<NutrientGroupDto?> GetByIdAsync(int id)
        {
            var group = await _repo.GetWithDetailsAsync(id);
            if (group is null)
            { throw new NotFoundDomainException($"NutrientGroup with ID {id} not found."); }

            return group.ToDto();
        }

        // Create a new nutrient group
        public async Task<int> CreateAsync(NutrientGroupCreateDto dto)
        {
            var name = dto.NutrientGroupName?.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("NutrientGroup name is required.");

            // Check for duplicate names
            if (await _repo.ExistsAsync(n => n.NutrientGroupName.ToLower() == name.ToLower()))
                throw new ConflictDomainException($"NutrientGroup '{name}' already exists.");

            var entity = dto.ToEntity();
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return entity.NutrientGroupId;
        }

        // Update an existing nutrient group
        public async Task<bool> UpdateAsync(int id, NutrientGroupUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundDomainException($"NutrientGroup with ID {id} not found.");
            dto.MapToEntity(entity);
            _repo.Update(entity);
            await _repo.SaveChangesAsync();
            return true;
        }

        // Delete a nutrient group
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundDomainException($"NutrientGroup with ID {id} not found.");
            _repo.Remove(entity);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
