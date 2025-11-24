namespace J3M.Shared.MealPlanModels
{
    // Holder for the weekly meal plan including daily summaries and a shopping list
    public class WeeklyMealPlanDto
    {
        public List<DayMealPlanDto> DaySummaries { get; set; }
        public List<string> ShoppingList { get; set; }
    }
}
