using J3m_BE.DTOs.Recipes;
using J3m_BE.Models;

namespace J3m_BE.Services.Interfaces;

public interface IRecipeService
{
    Task<IEnumerable<RecipeSummaryDto>> GetAllAsync();
    Task<RecipeDetailDto> GetByIdAsync(int id);
    Task<int> CreateAsync(RecipeCreateDto dto);
    Task<bool> UpdateAsync(int id, RecipeUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}