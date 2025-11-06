using J3m_BE.DTOs.Allergies;
using J3m_BE.Models;

namespace J3m_BE.Repositories.Interfaces
{
    // Allergy repository interface extending generic repository
  
    public interface IAllergyRepository : IGenericRepository<Allergy>
    {
        // Count all ingredients conntected to all allergies / a table showing count and allergy
        Task<List<AllergyDto?>> GetAllAllergiesWithCountAsync();

        // Get by Id what ingredients have this allergen
        Task<IEnumerable<Allergy?>> GetWithIngredientsAsync(int id);
      

    }
}
