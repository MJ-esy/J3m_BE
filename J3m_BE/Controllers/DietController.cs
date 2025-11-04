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
        public async Task<ActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        //Get: api/diet/5 - Returns a specific diet ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DietDto>> GetById(int id) =>
            await _service.GetByIdAsync(id) is DietDto diet ? Ok(diet) : NotFound();

        //Post: api/diet - Create a new diet and return it´s ID
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDietDto dto) =>
            await _service.CreateAsync(dto) is DietDto createdDiet
                ? CreatedAtAction(nameof(GetById), new { id = createdDiet.DietId }, createdDiet)
                : BadRequest("Diet could not be created.");

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
