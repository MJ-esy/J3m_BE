using J3M.Shared.DTOs.Recipes;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace J3m_BE.Controllers;

// Controller for managing recipes

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    // Dependency injection of the recipe service
    private readonly IRecipeService _service;

    public RecipesController(IRecipeService service)
        => _service = service;

    // GET: api/recipes
    [HttpGet]
    public async Task<ActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    // GET: api/recipes/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id) =>
        Ok(await _service.GetByIdAsync(id));

    // POST: api/recipes
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] RecipeCreateDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    // PUT: api/recipes/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] RecipeUpdateDto dto) =>
        await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();

    // DELETE: api/recipes/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id) =>
        await _service.DeleteAsync(id) ? NoContent() : NotFound();

    //POST: api/recipes/filterWithIngredients
    [HttpPost("filterWithIngredients")]
    public async Task<ActionResult> Filter([FromBody] IEnumerable<int> ingredientIds) =>
        Ok(await _service.FilterByIngredientsAsync(ingredientIds));
}