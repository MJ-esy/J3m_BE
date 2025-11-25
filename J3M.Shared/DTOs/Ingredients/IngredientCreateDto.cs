
using System.ComponentModel.DataAnnotations;

namespace J3M.Shared.DTOs.Ingredients
{

    // DTO for creating a new ingredient

    public class IngredientCreateDto
    {
        [Required, MaxLength(100)]
        public string IngredientName { get; set; } = string.Empty;
        public int? FoodGroupId { get; set; }

        // Options to link with existing entities
        public List<int>? AllergyIds { get; set; }
        public List<int>? NutrientGroupIds { get; set; }

    }
}