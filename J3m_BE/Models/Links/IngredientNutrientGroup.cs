namespace J3m_BE.Models.Links;

// Joined table for Ingredient and NutrientGroup

public class IngredientNutrientGroup
{
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
    
    public int NutrientGroupId { get; set; }
    public NutrientGroup NutrientGroup { get; set; } = null!;
}