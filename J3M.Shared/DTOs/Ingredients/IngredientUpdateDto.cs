
namespace J3M.Shared.DTOs.Ingredients;

// DTO for updating an existing ingredient

public class IngredientUpdateDto
{
    public string IngredientName { get; set; } = string.Empty;
    public int? FoodGroupId { get; set; }

    // Options to update links with existing entities
    public List<int>? AllergyIds { get; set; }
    public List<int>? NutrientGroupIds { get; set; }
}