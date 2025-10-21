using System.ComponentModel.DataAnnotations;
using J3m_BE.Models.Links;

namespace J3m_BE.Models;

// Model representing a diet category

public class Diet
{
    [Key]
    public int DietId { get; set; }
    
    [Required, MaxLength(100)]
    public string DietName { get; set; } = string.Empty;
    
    // Navigation property for related ingredient-diet links
    public ICollection<DietRecipe> RecipeLinks { get; set; } = new List<DietRecipe>();
}