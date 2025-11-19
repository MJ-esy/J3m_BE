namespace J3m_BE.DTOs.Recipes;

// Helper DTO for creating/updating recipes

public class IngredientAmountDto
{
    public int IngredientId { get; set; }
    public string? Measurement { get; set; }
}