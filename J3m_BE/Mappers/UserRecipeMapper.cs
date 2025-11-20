using global::J3m_BE.Models.Links;
using J3M.Shared.DTOs.UserRecipes;

namespace J3m_BE.Mappers;

public static class UserRecipeMapper
{
    public static UserRecipeDto ToDto(this UserRecipe ur)
    {
        return new UserRecipeDto
        {
            RecipeId = ur.RecipeId,
            RecipeName = ur.Recipe?.RecipeName ?? string.Empty,
            IsFavorite = ur.IsFavorite
        };
    }

    public static UserRecipeDetailDto ToDetailDto(this UserRecipe ur)
    {
        return new UserRecipeDetailDto
        {
            RecipeId = ur.RecipeId,
            RecipeName = ur.Recipe?.RecipeName ?? string.Empty,
            Description = ur.Recipe?.Description,
            PrepTimeMinutes = ur.Recipe?.PrepTimeMinutes ?? 0,
            ImageUrl = ur.Recipe?.ImageUrl,
            IsFavorite = ur.IsFavorite
        };
    }
}
