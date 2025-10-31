using J3m_BE.DTOs.FoodGroups;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace J3m_BE.Controllers;

// Controller for managing food groups

[ApiController]
[Route("api/[controller]")]
public class FoodGroupsController : ControllerBase
{
    // Dependency injection of the food group service
    private readonly IFoodGroupService _service;
    public FoodGroupsController(IFoodGroupService service) 
        => _service = service;
    
    // GET: api/foodgroups
    [HttpGet]
    public async Task<ActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());
    
    // GET: api/foodgroups/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id) =>
        Ok(await _service.GetByIdAsync(id));
    
    // POST: api/foodgroups
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] FoodGroupCreateDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }
    
    // PUT: api/foodgroups/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] FoodGroupUpdateDto dto) =>
        await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();
    
    // DELETE: api/foodgroups/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id) =>
        await _service.DeleteAsync(id) ? NoContent() : NotFound();
}