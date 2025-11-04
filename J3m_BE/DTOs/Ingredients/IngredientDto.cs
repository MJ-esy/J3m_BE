namespace J3m_BE.DTOs.Ingredients;

// DTO for basic ingredient information
// Used for listing ingredients with minimal details (basic display)

public class IngredientDto
{
    public int IngredientId { get; set; }
    public string IngredientName { get; set; } = string.Empty;
    public string? FoodGroupName { get; set; }
}