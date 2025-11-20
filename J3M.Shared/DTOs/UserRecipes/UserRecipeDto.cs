using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3M.Shared.DTOs.UserRecipes
{
    public class UserRecipeDto
    {
        public string UserId { get; set; }
        public int RecipeId { get; set; }
        public string RecipeName { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }
    }
}