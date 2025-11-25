using J3M.Shared.DTOs.Allergies;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace J3m_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AllergyController : ControllerBase
    {
        private readonly IAllergyService _service;
        public AllergyController(IAllergyService service)
            => _service = service;


        // Get api/allergies
        [HttpGet]
        public async Task<ActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        // Get api/allergies/6
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id) =>
            Ok(await _service.GetByIdAsync(id));

        // Post api/allergies
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AllergyCreateDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        // Put api/allergies/6
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AllergyUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            return success ? Ok() : NotFound();
        }

        // Delete api/alleries/6
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }

    }
}
