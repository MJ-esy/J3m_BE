using System.ComponentModel.DataAnnotations;
using J3m_BE.Models.Links;

namespace J3m_BE.Models;

// Model representing a regular user

public class User
{
    [Key]
    public string UserId { get; set; }
    
    [Required, MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    // Navigation property for the many-to-many relationship with Recipe
    public ICollection<UserRecipe> UserRecipes { get; set; } = new List<UserRecipe>();
}