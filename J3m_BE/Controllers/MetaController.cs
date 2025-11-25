using J3M.Shared.DTOs.Allergies;
using J3M.Shared.DTOs.Diets;
using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// Controller to handle metadata related requests such as allergies and diets

[Route("api/meta")]
[ApiController]
public class MetaController : ControllerBase
{
    private readonly IAllergyService _allergyService;
    private readonly IDietService _dietService;

    public MetaController(IAllergyService allergyService, IDietService dietService)
    {
        _allergyService = allergyService;
        _dietService = dietService;
    }

    [HttpGet("allergies")]
    public async Task<ActionResult<IEnumerable<AllergyDto>>> GetAllergies()
    {
        var allergies = await _allergyService.GetAllAsync();
        return Ok(allergies);
    }

    [HttpGet("diets")]
    public async Task<ActionResult<IEnumerable<DietDto>>> GetDiets()
    {
        var diets = await _dietService.GetAllAsync();
        return Ok(diets);
    }
}
