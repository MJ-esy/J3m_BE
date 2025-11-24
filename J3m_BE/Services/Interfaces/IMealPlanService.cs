using J3M.Shared.MealPlanModels;

namespace J3m_BE.Services.Interfaces
{
    public interface IMealPlanService
    {
        // Filter Recipes by allergies and diets IDs
        Task<List<DayMealPlanDto>> FilterRecipeAsync(List<int> allergyIds, List<int> dietIds);

        // Create Weekly Meal Plan with AI
        Task<WeeklyMealPlanDto> CreateWeeklyMealPlanWithAiAsync(List<int> allergyIds, List<int> dietIds);

    }
}
