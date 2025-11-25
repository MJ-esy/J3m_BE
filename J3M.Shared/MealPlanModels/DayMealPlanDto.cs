namespace J3M.Shared.MealPlanModels
{
    //Holder for meals for a specific day
    public class DayMealPlanDto
    {
        public string Day { get; set; }
        public List<MealSlotDto> Meals { get; set; }
        public string Summary { get; set; }
    }
}
