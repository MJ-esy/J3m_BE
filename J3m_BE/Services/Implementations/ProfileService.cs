using J3m_BE.DTOs.Users;
using J3m_BE.DTOs.Users.ProfileDtos;
using J3m_BE.Exceptions;
using J3m_BE.Mappers;
using J3m_BE.Models;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace J3m_BE.Services.Implementations
{   //Implementation of logic for "Myp Profile"
    public class ProfileService : IProfileService
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // Get users own profile as UserProfileDto
        public async Task<UserProfileDto> GetProfileAsync(string userId)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId) 
                ?? throw new NotFoundDomainException("User not found");
                return user.ToProfileDto();
        }

        //Update logged in users profile info details
        public async Task UpdateProfileAscync(string userId, UpdateProfileDto dto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("User not found");

            var normalizedEmail = _userManager.NormalizeEmail(dto.Email);
            var normalizedUserName = _userManager.NormalizeName(dto.UserName);

            var emailOwner = await _userManager.Users
               .FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail && u.Id != userId);

            if (emailOwner is not null) throw new ConflictDomainException("Email is already use");

            var userNameOwner = await _userManager.Users
               .FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName && u.Id != userId);

            if (userNameOwner is not null) throw new ConflictDomainException("UserName is already use");

            dto.MapToEntity(user);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new DomainException(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        // Change logged in users password
        public async Task ChangePasswordAsync(string userId, ChangePasswordDto dto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new NotFoundDomainException("User not found.");

            if (dto.NewPassword != dto.ConfirmNewPassword)
                throw new DomainException("New password and confirmation do not match.");

            var result = await _userManager.ChangePasswordAsync(
                user,
                dto.CurrentPassword,
                dto.NewPassword);

            if (!result.Succeeded)
                throw new DomainException(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        // Delete logged in users account
        public async Task DeleteAccountAsync(string userId)
        {
            var user =  _userManager.Users
                .FirstOrDefault(u => u.Id == userId) ?? throw new NotFoundDomainException("User not found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new DomainException(string.Join("; ", result.Errors.Select(e => e.Description)));
        }
    }
}
