using System.ComponentModel.DataAnnotations;

namespace J3m_BE.Models.Links;

// Joined table for Ingredient and Recipe

public class IngredientRecipe
{
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
    
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    
    [MaxLength(200)]
    public string? Measurements { get; set; }
}