using J3m_BE.DTOs.Ingredients;

namespace J3m_BE.Services.Interfaces;

// Ingredient Service Interface

public interface IIngredientService
{
    Task<IEnumerable<IngredientDto>> GetAllAsync();
    Task<IngredientDetailDto> GetByIdAsync(int id);
    Task<int> CreateAsync(IngredientCreateDto dto);
    Task<bool> UpdateAsync(int id, IngredientUpdateDto dto);
    Task<bool> DeleteAsync(int id);

    // to switch from string ingredient to ID to search the DB
    Task<List<int>> ResolveIdsByNamesAsync(IEnumerable<string> names);
}