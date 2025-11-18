using J3m_BE.DTOs.NutrientGroups;
using Microsoft.AspNetCore.Mvc;

namespace J3m_BE.Controllers
// Controller for managing Nutrient Groups
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutrientGroupController : ControllerBase
    {
        // Dependency injection of the nutrient group service
        private readonly Services.Interfaces.INutrientGroupService _service;
        public NutrientGroupController(Services.Interfaces.INutrientGroupService service)
            => _service = service;

        // GET: api/nutrientgroup - Get all nutrient groups
        [HttpGet]
        public async Task<ActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        // GET: api/nutrientgroup/5 - Get a nutrient group by ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id) =>
            Ok(await _service.GetByIdAsync(id));

        // POST: api/nutrientgroup - Create a new nutrient group
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] NutrientGroupCreateDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        //PUT: api/nutrientgroup/5 - Update an existing nutrient group
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] NutrientGroupUpdateDto dto) =>
            await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();

        // DELETE: api/nutrientgroup/5 - Delete a nutrient group
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) =>
            await _service.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
