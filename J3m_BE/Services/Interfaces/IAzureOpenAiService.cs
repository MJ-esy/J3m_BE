using J3M.Shared.MealPlanModels;

namespace J3m_BE.Services.Interfaces
{
    public interface IAzureOpenAiService
    {
        Task<WeeklyMealPlanDto> EnrichAsync(List<DayMealPlanDto> plan, List<int> allergyIds, List<int> dietIds);
    }
}
