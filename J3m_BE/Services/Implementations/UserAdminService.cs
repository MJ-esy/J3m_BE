using J3m_BE.DTOs.Users.AdminDtos;
using J3m_BE.Exceptions;
using J3m_BE.Models;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace J3m_BE.Services.Implementations
{
    // Handles admin-level user management:
    // creating users, setting roles, and deleting accounts.
    public class UserAdminService : IUserAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAdminService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Admin creates a new user and assigns roles.
        public async Task<string> CreateUserAsync(CreateUserByAdminDto dto)
        {
            var user = new AppUser
            {
                Email = dto.Email.Trim(),
                UserName = dto.UserName.Trim(),
                DisplayName = dto.DisplayName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new DomainException(string.Join("; ", result.Errors.Select(e => e.Description)));

            if (dto.Roles.Any())
                await SetRolesAsync(user.Id, dto.Roles);

            return user.Id;
        }

       
        // Replaces all roles for a specific user with the provided list.

        public async Task SetRolesAsync(string userId, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NotFoundDomainException("User not found.");

            var currentRoles = await _userManager.GetRolesAsync(user);

            // Ensure each target role exists
            foreach (var role in roles.Distinct())
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }

            // Replace existing roles with the new set
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRolesAsync(user, roles);
        }

       
        // Permanently deletes a user account.
        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NotFoundDomainException("User not found.");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new DomainException(string.Join("; ", result.Errors.Select(e => e.Description)));
        }
    }
    
}
