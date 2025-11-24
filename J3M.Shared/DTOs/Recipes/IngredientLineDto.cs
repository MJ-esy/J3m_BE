namespace J3M.Shared.DTOs.Recipes;

// Helper DTO for returning ingredient lines in recipe details

public class IngredientLineDto
{
    public int IngredientId { get; set; }
    public string IngredientName { get; set; } = string.Empty;
    public string Measurement { get; set; } = string.Empty;
}