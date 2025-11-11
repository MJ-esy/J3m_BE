namespace J3m_BE.Models.Links;

// Join table for Users and Recipe

public class UserRecipe
{
    public string UserId { get; set; } = default!;
    public int RecipeId { get; set; }

    public AppUser User { get; set; } = default!;
    public Recipe Recipe { get; set; } = default!;
}
