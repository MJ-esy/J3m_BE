using System.ComponentModel.DataAnnotations;
using J3m_BE.Models.Links;

namespace J3m_BE.Models;

// Model representing a nutrient group category

public class NutrientGroup
{
    [Key]
    public int NutrientGroupId { get; set; }
    
    [Required, MaxLength(100)]
    public string NutrientGroupName { get; set; } = string.Empty;
    
    // Navigation property for related ingredient-nutrient group links
    public ICollection<IngredientNutrientGroup> IngredientLinks { get; set; } = new List<IngredientNutrientGroup>();
}