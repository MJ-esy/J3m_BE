using J3m_BE.DTOs.Recipes;

namespace J3M.Shared.MealPlanModels
{
    //Holder for a specific meal slot/recipe
    public class MealSlotDto
    {
        public string MealType { get; set; } //Lunch, Dinner
        public RecipeDetailDto Recipe { get; set; }
    }
}
