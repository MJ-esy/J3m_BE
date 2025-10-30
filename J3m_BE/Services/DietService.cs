using J3m_BE.DTOs.Diets;
using J3m_BE.Exceptions;
using J3m_BE.Mappers;
using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;

namespace J3m_BE.Services
{
    public class DietService : IDietService
    {
        private readonly IDietRepository _repo;
        public DietService(IDietRepository repo)
        {
            _repo = repo;
        }

        // Get all Diet entities from repository and maps them to DietDto.
        //Uses service-layer mapping for flexibility and testability.
        public async Task<IEnumerable<DietDto>> GetAllAsync()
        {
            var diets = await _repo.GetAllAsync();
            return diets.Select(d => d.ToDto());
        }

        //Efficent projection - no mapping needed - Get all diets with the number of linked recipes.
        public async Task<IEnumerable<DietWithCountDto>> GetDietsWithRecipeCountAsync()
        {
            return await _repo.GetDietWithRecipeCountAsync();

        }

        //Already projected to DTOs in repo - Get all recipe linked to a specific diet.
        public async Task<IEnumerable<Recipe>> GetRecipesByDietAsync(int id)
        {
            return await _repo.GetRecipesByDietAsync(id);
        }

        //Get a single Diet by Id (maps it to DietDto)
        public async Task<DietDto?> GetByIdAsync(int id)
        {
            var diet = await _repo.GetByIdAsync(id);
            if (diet is null)
                throw new NotFoundDomainException($"Diet with ID {id} was not found");
            return diet.ToDto();
        }
        //Create a new diet
        public async Task<DietDto?> CreateAsync(CreateDietDto dto)
        {
           var dietname = dto.DietName?.Trim();
            if (string.IsNullOrWhiteSpace(dto.DietName))
                throw new DomainException("Diet name is required");


            var entity = dto.ToEntity();
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return entity.ToDto();
        }

        //Update an existing diet with new data.
        public async Task<bool> UpdateAsync(int id, UpdateDietDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null)return  false;

            dto.MapToEntity(entity);
            _repo.Update(entity);
            await _repo.SaveChangesAsync();
            return true;
        }

        //Delete a diet it´s ID
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
