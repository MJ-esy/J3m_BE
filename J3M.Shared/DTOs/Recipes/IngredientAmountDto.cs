namespace J3M.Shared.DTOs.Recipes;

// Helper DTO for creating/updating recipes

public class IngredientAmountDto
{
    public int IngredientId { get; set; }
    public string? Measurement { get; set; }
}