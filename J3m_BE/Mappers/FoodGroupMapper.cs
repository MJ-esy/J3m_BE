using J3m_BE.DTOs.FoodGroups;
using J3m_BE.Models;

namespace J3m_BE.Mappers;

// Mapper class for converting between FoodGroup entities and DTOs

public static class FoodGroupMapper
{
    // Convert FoodGroup entity to FoodGroupDto
    public static FoodGroupDto ToDto(this FoodGroup entity)
    {
        return new FoodGroupDto
        {
            FoodGroupId = entity.FoodGroupId,
            FoodGroupName = entity.FoodGroupName,
            IngredientCount = entity.Ingredients?.Count ?? 0
        };
    }

    // Convert FoodGroupCreateDto to FoodGroup entity
    public static FoodGroup ToEntity(this FoodGroupCreateDto dto)
    {
        return new FoodGroup
        {
            FoodGroupName = dto.FoodGroupName.Trim()
        };
    }
    
    // Update existing FoodGroup entity with data from FoodGroupUpdateDto
    public static void MapToEntity(this FoodGroupUpdateDto dto, FoodGroup entity)
    {
        entity.FoodGroupName = dto.FoodGroupName.Trim();
    }
}