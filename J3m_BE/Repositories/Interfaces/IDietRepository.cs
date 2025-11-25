using J3M.Shared.DTOs.Diets;
using J3m_BE.Models;


namespace J3m_BE.Repositories.Interfaces
{
    public interface IDietRepository : IGenericRepository<Diet>
    {
        Task<Diet?> GetWithDetailsAsync(int id);

        Task<IEnumerable<DietWithCountDto>> GetDietWithRecipeCountAsync();
    }
}
