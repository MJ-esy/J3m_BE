using J3M.Shared.MealPlanModels;

namespace J3m_BE.Services.Interfaces
{
    public interface IMealPlanService
    {
        Task<List<DayMealPlanDto>> FilterRecipeAsync(List<int> allergyIds, List<int> dietIds);

    }
}
