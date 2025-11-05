using J3m_BE.DTOs.FoodGroups;
using J3m_BE.Models;

namespace J3m_BE.Services.Interfaces;

// Interface for food group related operations
// Returning DTOs to decouple service layer from data models

public interface IFoodGroupService
{
    Task<IEnumerable<FoodGroupDto>> GetAllAsync();
    Task<FoodGroupDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(FoodGroupCreateDto dto);
    Task<bool> UpdateAsync(int id, FoodGroupUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}