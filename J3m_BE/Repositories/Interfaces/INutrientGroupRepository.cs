using J3m_BE.DTOs.NutrientGroups;
using J3m_BE.Models;

namespace J3m_BE.Repositories.Interfaces
{

    //Interface for Nutrient Group data access operations

    public interface INutrientGroupRepository : IGenericRepository<NutrientGroup>
    {
        Task<NutrientGroup?> GetWithDetailsAsync(int id);
        Task<IEnumerable<NutrientGroupDto>> GetAllWithIngredientsCountAsync();
    }
}
