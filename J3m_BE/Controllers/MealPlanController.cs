using J3M.Shared.MealPlanModels;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// Controller for handling meal plan related requests.
namespace J3m_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly IMealPlanService _mealPlanService;

        public MealPlanController(IMealPlanService mealPlanService) => _mealPlanService = mealPlanService;

        // Endpoint to get a weekly meal plan based on allergies and diets.
        [HttpPost("weekly")]
        public async Task<IActionResult> GetWeeklyMealPlan([FromBody] MealPlanRequest request)
        {
            var plan = await _mealPlanService.FilterRecipeAsync(request.AllergyIds, request.DietIds);
            return Ok(plan);
        }
        // Endpoint to get a weekly meal plan enriched with AI based on allergies and diets.
        [HttpPost("weekly/ai")]
        public async Task<IActionResult> GetWeeklyMealPlanWithAi([FromBody] MealPlanRequest request)
        {
            var enriched = await _mealPlanService.CreateWeeklyMealPlanWithAiAsync(request.AllergyIds, request.DietIds);
            return Ok(enriched);
        }
    }
}
