// DTO for basic ingredient information
// Used for listing ingredients with minimal details (basic display)

namespace J3M.Shared.DTOs.Ingredients;

public class IngredientDto
{
    public int IngredientId { get; set; }
    public string IngredientName { get; set; } = string.Empty;
    public string? FoodGroupName { get; set; }
}