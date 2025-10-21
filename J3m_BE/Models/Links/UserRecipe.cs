namespace J3m_BE.Models.Links;

// Join table for Users and Recipe

public class UserRecipe
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    
    public bool IsFavorite { get; set; }
}