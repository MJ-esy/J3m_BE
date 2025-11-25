namespace J3M.Shared.MealPlanModels
{
    // Request model for meal plan generation
    public class MealPlanRequest
    {
        public List<int>? AllergyIds { get; set; } = null;
        public List<int>? DietIds { get; set; } = null;
    }
}
