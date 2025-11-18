using J3m_BE.DTOs.Users.ProfileDtos;
using J3m_BE.Mappers;
using J3m_BE.Models;
using J3m_BE.Services.Common;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace J3m_BE.DTOs.Users;

[ApiController]
[Route("api/[controller]")]
[Authorize]

// Controller for managing user profiles
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly ICurrentUserService _current;

    public ProfileController(IProfileService profileService, ICurrentUserService current)
    {
        _profileService = profileService;
        _current = current;
    }

    // GET: /api/Profile/me
    //Get logged-in user's profile
    [HttpGet("me")]
    public async Task<ActionResult<UserProfileDto>> GetMyProfile()
    {
        var userId = _current.UserId;
        if(userId is null)
            return Unauthorized();

        var profile = await _profileService.GetProfileAsync(userId);
        return Ok(profile);
    }

    // PUT: api/Profile/me
    // Update logged-in user's profile ïnfo (UserName, Email, DisplayName)
    [HttpPut("me")]
    public async Task<ActionResult> UpdateMyProfile([FromBody] UpdateProfileDto dto)
    {
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);

        var userId = _current.UserId;
        if(userId is null)
            return Unauthorized();

        await _profileService.UpdateProfileAscync(userId, dto);
        return NoContent();

    }

    //PUT: api/Profile/me/password
    // Change logged-in user's password
    [HttpPut("change/password")]
    public async Task<ActionResult> ChangeMyPassword([FromBody] ChangePasswordDto dto) 
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        var userId = _current.UserId;
        if (userId is null)
            return Unauthorized();
        await _profileService.ChangePasswordAsync(userId, dto);
        return NoContent();

    }

    // Delete api/Profile
    // Delete logged-in user's account permenently
    [HttpDelete]
    public async Task<ActionResult> DeleteMyAccount()
    {
        var userId = _current.UserId;
        if (userId is null)
            return Unauthorized();
        await _profileService.DeleteAccountAsync(userId);
        return NoContent();
    }

}
