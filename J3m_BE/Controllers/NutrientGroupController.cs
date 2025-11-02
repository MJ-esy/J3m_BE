using J3m_BE.DTOs.NutrientGroups;
using Microsoft.AspNetCore.Http;
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

        // GET: api/nutrientgroup
        [HttpGet]
        public async Task<ActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        // GET: api/nutrientgroup/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id) =>
            Ok(await _service.GetByIdAsync(id));

        // POST: api/nutrientgroup
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateNutrientGroupDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        //PUT: api/nutrientgroup/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateNutrientGroupDto dto) =>
            await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();

        // DELETE: api/nutrientgroup/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) =>
            await _service.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
