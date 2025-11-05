using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using J3m_BE.Models.Links;

namespace J3m_BE.Models;

// Model representing an ingredient used in recipes

public class Ingredient
{
    [Key]
    public int IngredientId { get; set; }
    
    [Required, MaxLength(100)]
    public string IngredientName { get; set; } = string.Empty;
    
    [ForeignKey(nameof(FoodGroup))]
    public int? FoodGroupId { get; set; }
    public FoodGroup? FoodGroup { get; set; }

    // Navigation property for the many-to-many relationships
    public ICollection<IngredientRecipe> RecipeLinks { get; set; } = new List<IngredientRecipe>();
    public ICollection<IngredientAllergy> AllergyLinks { get; set; } = new List<IngredientAllergy>();
    public ICollection<IngredientNutrientGroup> NutrientLinks { get; set; } = new List<IngredientNutrientGroup>();
}