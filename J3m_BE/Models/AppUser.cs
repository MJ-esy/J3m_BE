using J3m_BE.Models.Links;
using Microsoft.AspNetCore.Identity;

namespace J3m_BE.Models
{
    // Extending IdentityUser to include additional properties for application users
    public class AppUser : IdentityUser
    {
        public string? DisplayName { get; set; }

        // Navigation property for the many-to-many relationship with Recipe
        public ICollection<Recipe> CreateRecipes { get; set; } = new List<Recipe>();
        public ICollection<UserRecipe> UserRecipes { get; set; } = new List<UserRecipe>();
    }
}
