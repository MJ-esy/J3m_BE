using J3m_BE.DTOs.Users.ProfileDtos;
using J3m_BE.Mappers;
using J3m_BE.Models;
using J3m_BE.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace J3m_BE.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User,Admin")]

// Controller for managing user profiles
public class ProfileController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICurrentUserService _current;

    public ProfileController(UserManager<AppUser> userManager, ICurrentUserService current)
    {
        _userManager = userManager;
        _current = current;
    }

    // GET: api/profile
    [HttpGet]
    public async Task<ActionResult<ProfileDto>> Me()
    {
        if (string.IsNullOrWhiteSpace(_current.UserId)) return Unauthorized();

        var user = await _userManager.FindByIdAsync(_current.UserId);
        return user is null ? NotFound() : Ok(user.ToProfileDto());
    }

    // PUT: api/profile
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProfileDto dto)
    {
        // With [ApiController], invalid DTOs auto-return 400—no need to check ModelState
        if (string.IsNullOrWhiteSpace(_current.UserId)) return Unauthorized();

        var user = await _userManager.FindByIdAsync(_current.UserId);
        if (user is null) return NotFound();

        dto.MapToEntity(user);

        var res = await _userManager.UpdateAsync(user);
        return res.Succeeded ? NoContent() : BadRequest(res.Errors);
    }
}
