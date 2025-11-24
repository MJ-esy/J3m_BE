using J3m_BE.DTOs.Ingredients;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace J3m_BE.Controllers;

// Controller for managing ingredients

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    // Dependency injection of the ingredient service
    private readonly IIngredientService _service;
    

    public IngredientsController(IIngredientService service)
    {
        _service = service;
      
    }

    // GET: api/ingredients
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }
    
    // GET: api/ingredients/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dto = await _service.GetByIdAsync(id); // throws if not found
        return Ok(dto);
    }
    
    // POST: api/ingredients
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IngredientCreateDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }
    
    // PUT: api/ingredients/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] IngredientUpdateDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto); // Throws if not found
        return updated ? NoContent() : NotFound();
    }
    
    // DELETE: api/ingredients/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id); // Throws Conflict if in use by Recipe
        return deleted ? NoContent() : NotFound();
    }

    // POST: api/ingredients/resolve
    [HttpPost("resolve")]
    public async Task<ActionResult<List<int>>> Resolve([FromBody] List<string> ingredientNames)
    {
        if (ingredientNames == null || ingredientNames.Count == 0)
            return BadRequest("No ingredient names provided.");

        var ids = await _service.ResolveIdsByNamesAsync(ingredientNames);
        return Ok(ids);
    }

}