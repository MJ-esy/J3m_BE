using J3m_BE.DTOs.NutrientGroups;
using J3m_BE.Models;

namespace J3m_BE.Mappers
{
    // Mapper class for converting between NutrientGroup entities and DTOs
    public static class NutrientGroupMapper
    {
        // Convert NutrientGroup entity to NutrientGroupDto
        public static NutrientGroupDto ToDto(this NutrientGroup entity)
        {
            return new NutrientGroupDto
            {
                NutrientGroupId = entity.NutrientGroupId,
                NutrientGroupName = entity.NutrientGroupName,
                IngredientCount = entity.IngredientLinks?.Count ?? 0
            };
        }

        // Convert CreateNutrientGroupDto to NutrientGroup entity
        public static NutrientGroup ToEntity(this NutrientGroupCreateDto dto)
        {
            return new NutrientGroup
            {
                NutrientGroupName = dto.NutrientGroupName.Trim()
            };
        }

        // Update existing FoodGroup entity with data from UpdateNutrientGroupDto
        public static void MapToEntity(this NutrientGroupUpdateDto dto, NutrientGroup entity)
        {
            entity.NutrientGroupName = dto.NutrientGroupName.Trim();
        }

    }
}
