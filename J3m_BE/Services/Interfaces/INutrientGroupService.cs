using J3M.Shared.DTOs.NutrientGroups;

namespace J3m_BE.Services.Interfaces
{
    public interface INutrientGroupService
    {
        // Interface for NutrientGroup related operations
        // Returning DTOs to decouple service layer from data models
        Task<IEnumerable<NutrientGroupDto>> GetAllAsync();
        Task<NutrientGroupDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(NutrientGroupCreateDto dto);
        Task<bool> UpdateAsync(int id, NutrientGroupUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}