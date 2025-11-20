using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3M.Shared.DTOs.UserRecipes
{
    public class UserRecipeDetailDto
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsFavorite { get; set; }
        public int PrepTimeMinutes { get; set; }
        public string? ImageUrl { get; set; }
    }
}
