using System.ComponentModel.DataAnnotations;

namespace J3m_BE.Models;

// Model representing a food group category

public class FoodGroup
{
    [Key]
    public int FoodGroupId { get; set; }
    
    [Required, MaxLength(100)]
    public string FoodGroupName { get; set; } = string.Empty;
    
    // Navigation property for related ingredients
    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}