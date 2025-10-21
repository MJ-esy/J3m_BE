namespace J3m_BE.Models.Links;

// Joined table for Ingredient and Allergy

public class IngredientAllergy
{
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
    
    public int AllergyId { get; set; }
    public Allergy Allergy { get; set; } = null!;
}