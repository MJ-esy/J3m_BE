using J3M.Shared.DTOs.Diets;
using J3m_BE.Models;

namespace J3m_BE.Mappers
{
    // Provides extension methods to convert between different representations of diet-related data
    // Helps translate between Entities (use in the database or domain model) and DTO (use for API responses or requests). 


    public static class DietMapper
    {
        //Convert Diet entity to a DietDto.Return basic diet info to the client.
        public static DietDto ToDto(this Diet entity)
        {
            return new DietDto
            {
                DietId = entity.DietId,
                DietName = entity.DietName,
            };
        }

        //Converts a Diet enity to DietWithCountDto. Show how many recipe are linked to each diet.
        public static DietWithCountDto ToCountDto(this Diet entity)
        {
            return new DietWithCountDto
            {
                DietId = entity.DietId,
                DietName = entity.DietName,
                RecipeCount = entity.RecipeLinks?.Count ?? 0 // ensure code doesn´t crash if RecipeLinks is missing
            };
        }

        //Converts a CreateDietDto (from a client request) into a Diet enity.  Creates a new Diet in the database
        public static Diet ToEntity(this DietCreateDto dto)
        {
            return new Diet
            {
                DietName = dto.DietName.Trim()
            };
        }

        //Updates existing Diet entity using data from an UpdateDietDto.
        public static void MapToEntity(this DietUpdateDto dto, Diet entity)
        {
            entity.DietName = dto.DietName.Trim();
        }

    }
}
