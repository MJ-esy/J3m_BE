namespace J3m_BE.DTOs.Recipes;

// DTO for creating a new recipe

public class RecipeUpdateDto
{
    // Basic properties
    public string? RecipeName { get; set; }
    public string? Description { get; set; }
    public int? PrepTimeMinutes { get; set; }
    public string? ImageUrl { get; set; }
    
    // Relationship IDs
    public List<IngredientAmountDto>? Ingredients { get; set; }
    public List<int>? DietIds { get; set; }
}