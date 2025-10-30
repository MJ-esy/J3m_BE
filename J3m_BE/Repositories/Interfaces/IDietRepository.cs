using J3m_BE.DTOs.Diets;
using J3m_BE.Models;


namespace J3m_BE.Repositories.Interfaces
{
    public interface IDietRepository : IGenericRepository<Diet>
    {
        Task <IEnumerable<DietWithCountDto>> GetDietWithRecipeCountAsync();
        Task<IEnumerable<Recipe>> GetRecipesByDietAsync(int id);

    }
}
