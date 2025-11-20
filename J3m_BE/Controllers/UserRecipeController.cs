using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace J3m_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require authentication
    public class UserRecipesController : ControllerBase
    {
        private readonly IUserRecipeService _service;

        public UserRecipesController(IUserRecipeService service)
        {
            _service = service;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }


        [HttpPost("{recipeId}/favorite")]
        public async Task<IActionResult> FavoriteRecipe(int recipeId)
        {
            var userId = GetUserId();
            await _service.FavoriteRecipeAsync(userId, recipeId);
            return Ok(new { message = "Recipe favorited successfully." });
        }

        [HttpDelete("{recipeId}/favorite")]
        public async Task<IActionResult> UnfavoriteRecipe(int recipeId)
        {
            var userId = GetUserId();
            var result = await _service.UnfavoriteRecipeAsync(userId, recipeId);
            if (!result) return NotFound();
            return Ok(new { message = "Recipe unfavorited successfully." });
        }

        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = GetUserId();
            var favorites = await _service.GetFavoriteRecipesAsync(userId);
            return Ok(favorites);
        }
    }


    }
