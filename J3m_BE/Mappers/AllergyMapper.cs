using J3m_BE.Models;
using J3m_BE.DTOs.Allergies;

namespace J3m_BE.Mappers
{
    // Mapper class for converting between Allergy entities and DTOs
    public static class AllergyMapper
    {
        // Convert Allergy entity to AllergyDto
        public static AllergyWithCountDto ToDto(this Allergy entity)
        {
            return new AllergyWithCountDto
            {
                AllergyId = entity.AllergyId,
                AllergyName = entity.AllergyName,
                IngredientCount = entity.IngredientLinks?.Count ?? 0 // Handle null case

            };
        }
        // Convert CreateAllergyDto to Allergy entity
        public static Allergy ToEntity(this AllergyCreateDto dto)
        {
            return new Allergy
            {
                AllergyName = dto.AllergyName.Trim()
            };
        }

        // Update existing Allergy entity with data from AllergyUpdateDto
        public static void MapToEntity(this AllergyUpdateDto dto, Allergy entity)
        {
            entity.AllergyName = dto.AllergyName.Trim();
        }

    }
}
