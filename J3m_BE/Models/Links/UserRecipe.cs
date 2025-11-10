namespace J3m_BE.Models.Links;

// Join table for Users and Recipe

public class UserRecipe
{
    public string UserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    
    public bool IsFavorite { get; set; }
}