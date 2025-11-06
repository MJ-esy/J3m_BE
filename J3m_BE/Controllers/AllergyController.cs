using Microsoft.AspNetCore.Mvc;
using J3m_BE.DTOs.Allergies;
using J3m_BE.Services.Interfaces;

namespace J3m_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AllergyController : ControllerBase
    {
       private readonly IAllergyService _service;
        public AllergyController(Services.Interfaces.IAllergyService services)
            => _services = services;


        // Get api/allergies
        [HttpGet]
        public async Task<ActionResult> GetAll() =>
            Ok(await _services.GetAllAllergiesAsync());

        // Get api/allergies/6
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id) =>
            Ok(await _services.GetAllergyByIdAsync(id));

        // Post api/allergies
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AllergyCreateDto dto)
        {
            var id = await _services.CreateAllergyAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id }, new {id});
        }

        // Put api/allergies/6
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AllergyUpdateDto dto)
        {
            var result = await _services.UpdateAllergyAsync(id, dto);
            return result == null ? NotFound() : Ok(result);
        }

        // Delete api/alleries/6
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _services.DeleteAllergyAsync(id);
            return success ? NoContent() : NotFound();
        }

    }
}
