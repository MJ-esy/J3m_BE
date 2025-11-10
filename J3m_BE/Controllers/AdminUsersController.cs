using J3m_BE.DTOs.Users.AdminDtos;
using J3m_BE.Models;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace J3m_BE.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]

// Controller for admin user management
public class AdminUsersController : ControllerBase
{
    private readonly IUserAdminService _adminService;
    private readonly UserManager<AppUser> _userManager;

    public AdminUsersController(IUserAdminService adminService, UserManager<AppUser> userManager)
    {
        _adminService = adminService;
        _userManager = userManager;
    }

    //Create a user (admin action).
    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateUserByAdminDto dto)
        => Ok(await _adminService.CreateUserAsync(dto));

    //Replace roles for a user (admin action).
    [HttpPut("{userId}/roles")]
    public async Task<IActionResult> SetRoles(string userId, SetRolesDto dto)
    {
        await _adminService.SetRolesAsync(userId, dto.Roles);
        return NoContent();
    }

    //Delete a user (admin action).
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(string userId)
    {
        await _adminService.DeleteUserAsync(userId);
        return NoContent();
    }

    //List users (light projection).
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userManager.Users.Select(u => new { u.Id, u.UserName, u.Email });
        return Ok(users);
    }
}
