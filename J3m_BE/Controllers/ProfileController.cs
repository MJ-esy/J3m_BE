using J3m_BE.DTOs.Users;
using J3m_BE.Mappers;
using J3m_BE.Models;
using J3m_BE.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace J3m_BE.DTOs.Users;

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
    public async Task<ActionResult<UserProfileDto>> Me()
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

        //Email changes must go through UserManager for normalization/validation

        if(!string.IsNullOrWhiteSpace(dto.Email) && !string.Equals(dto.Email.Trim(), user.Email, StringComparison.OrdinalIgnoreCase))
        {
            var setEmail = await _userManager.SetEmailAsync(user, dto.Email.Trim());
            if (!setEmail.Succeeded)
                return BadRequest(setEmail.Errors);
        }

        //DisplayName is custom field; safe to set then UpdateAsync 
        if (!string.IsNullOrWhiteSpace(dto.DisplayName))
        {
            user.DisplayName = dto.DisplayName.Trim();
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
        }
        return NoContent();

    }
}
