using J3m_BE.Models.Links;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace J3m_BE.Models;

// Model representing a recipe

public class Recipe
{
    [Key]
    public int RecipeId { get; set; }
    
    [Required, MaxLength(100)]
    public string RecipeName { get; set; } = string.Empty;
    
    [MaxLength(4000)]
    public string? Description { get; set; }

    [ForeignKey(nameof(CreatedByUser))]
    public string? CreatedByUserId { get; set; }

    [InverseProperty(nameof(AppUser.CreateRecipes))]
    public AppUser? CreatedByUser { get; set; }
    
    [Range(0, 1440)] // ADDED: Constraint for realistic prep time
    public int PrepTimeMinutes { get; set; }
    
    [MaxLength(1024)]
    [Url] // ADDED: Validate URL format
    public string? ImageUrl { get; set; }
    
    // Navigation property for the many-to-many relationships
    public ICollection<IngredientRecipe> IngredientLinks { get; set; } = new List<IngredientRecipe>();
    public ICollection<DietRecipe> DietLinks { get; set; } = new List<DietRecipe>();
    public ICollection<UserRecipe> UserLinks { get; set; } = new List<UserRecipe>();


}