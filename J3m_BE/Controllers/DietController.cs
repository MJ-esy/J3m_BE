using J3m_BE.DTOs.Diets;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace J3m_BE.Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class DietController : ControllerBase
    {
        // Dependency injection of Diet Service
        private readonly IDietService _service;
        public DietController(IDietService service) => _service = service;


        //Get: api/diet - Returns all diets in the sysmtem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DietDto>>> GetAll()
        {
            var diets = await _service.GetAllAsync();
            return Ok(diets);
        }

        //Get: api/diet/5 - Returns a specific diet ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DietDto>> GetById(int id)
        {
            var diet = await _service.GetByIdAsync(id);
            return diet is null ? NotFound() : Ok(diet);
        }

        //Post: api/diet - Create a new diet and return it´s ID
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDietDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        //Put: api/diet/5 - Update an existing diet; returns 204 if successful, 404 if not found
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateDietDto dto) =>
            await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();

        //Delete: api/delete - Deletes a diet
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) =>
            await _service.DeleteAsync(id) ? NoContent() : NotFound();

    }
}
