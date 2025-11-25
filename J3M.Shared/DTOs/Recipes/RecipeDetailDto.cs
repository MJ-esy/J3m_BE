namespace J3M.Shared.DTOs.Recipes;

// DTO for detailed recipe information

public class RecipeDetailDto
{
    // Basic properties
    public int RecipeId { get; set; }
    public string RecipeName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int PrepTimeMinutes { get; set; }
    public string? ImageUrl { get; set; }

    // Navigation properties
    public IEnumerable<string> Diets { get; set; } = new List<string>();
    public IEnumerable<IngredientLineDto> Ingredients { get; set; } = new List<IngredientLineDto>();
}