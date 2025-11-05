using System.ComponentModel.DataAnnotations;
using J3m_BE.Models.Links;

namespace J3m_BE.Models;

// Model representing a both regular user and Admin user

public class User
{
    [Key]
    public int UserId { get; set; }
    
    [Required, MaxLength(100)]
    public string Username { get; set; } = string.Empty;
    [Required, MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = "User";

    
    // Navigation property for the many-to-many relationship with Recipe
    public ICollection<Recipe> CreateRecipes {  get; set; } = new List<Recipe>();
    public ICollection<UserRecipe> UserRecipes { get; set; } = new List<UserRecipe>();
}