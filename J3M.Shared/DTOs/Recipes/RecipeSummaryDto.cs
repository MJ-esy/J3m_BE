namespace J3m_BE.DTOs.Recipes;

// DTO for recipe summary information

public class RecipeSummaryDto
{
    // Basic properties
    public int RecipeId { get; set; }
    public string RecipeName { get; set; } = string.Empty;
    public int IngredientCount { get; set; }
    public int DietCount { get; set; }
    public int PrepTimeMinutes { get; set; }
}