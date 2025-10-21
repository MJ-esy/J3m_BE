namespace J3m_BE.Models.Links;

// Joined table for Diet and Recipe

public class DietRecipe
{
    public int DietId { get; set; }
    public Diet Diet { get; set; } = null!;
    
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
}