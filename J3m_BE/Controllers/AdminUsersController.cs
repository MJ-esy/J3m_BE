
using J3M.Shared.DTOs.Users;
using J3M.Shared.DTOs.Users.AdminDtos;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace J3m_BE.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]

// Controller for admin user management
public class AdminUsersController : ControllerBase
{
    private readonly IUserAdminService _adminService;

    public AdminUsersController(IUserAdminService adminService)
    {
        _adminService = adminService;
    }

    // List users (light projection).
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserListItemDto>>> GetAll()
    {
        var users = await _adminService.GetAllAsync();
        return Ok(users);
    }

    // Create a user (admin action).
    [HttpPost]
    public async Task<ActionResult<string>> CreateUser(CreateUserByAdminDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var newUserId = await _adminService.CreateUserAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = newUserId }, newUserId);
    }

    // Replace roles for a user (admin action).
    [HttpPut("{userId}/roles")]
    public async Task<ActionResult> SetRoles(string userId, SetRolesDto dto)
    {
        await _adminService.SetRolesAsync(userId, dto.Roles);
        return NoContent();
    }

    // Delete a user permanently (admin action).
    [HttpDelete("{userId}")]
    public async Task<ActionResult> Delete(string userId)
    {
        await _adminService.DeleteUserAsync(userId);
        return NoContent();
    }
}
