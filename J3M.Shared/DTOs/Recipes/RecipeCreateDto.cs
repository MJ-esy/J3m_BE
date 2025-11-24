using System.ComponentModel.DataAnnotations;

namespace J3M.Shared.DTOs.Recipes;

// DTO for creating a new recipe

public class RecipeCreateDto
{
    // Basic properties
    [Required, MaxLength(100)]
    public string RecipeName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int PrepTimeMinutes { get; set; }
    public string? ImageUrl { get; set; }

    // Relationship IDs
    public List<IngredientAmountDto> Ingredients { get; set; } = new();
    public List<int> DietIds { get; set; } = new();
}