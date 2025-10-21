using System.ComponentModel.DataAnnotations;
using J3m_BE.Models.Links;

namespace J3m_BE.Models;

// Model representing an allergy category

public class Allergy
{
  [Key]
  public int AllergyId { get; set; }

  [Required, MaxLength(50)]
  public string AllergyName { get; set; } = string.Empty;
  
  // Navigation property for the many-to-many relationship with Ingredient
  public ICollection<IngredientAllergy> IngredientLinks { get; set; } = new List<IngredientAllergy>();

}