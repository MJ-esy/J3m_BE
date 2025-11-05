using J3m_BE.DTOs.FoodGroups;
using J3m_BE.Models;

namespace J3m_BE.Repositories.Interfaces;

// Interface for food group data access operations
// (Note: Future-proofing for potential additional methods)

public interface IFoodGroupRepository : IGenericRepository<FoodGroup>
{
    Task<FoodGroup?> GetWithIngredientsAsync(int id);
    Task<IEnumerable<FoodGroupDto>> GetAllWithIngredientsCountAsync();
}