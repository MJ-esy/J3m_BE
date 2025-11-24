using J3M.Shared.DTOs.Users.ProfileDtos;

namespace J3m_BE.Services.Interfaces
{
    public interface IProfileService
    {
        Task<UserProfileDto> GetProfileAsync(string userId);
        Task UpdateProfileAscync(string userId, UpdateProfileDto dto);
        Task ChangePasswordAsync(string userId, ChangePasswordDto dto);
        Task DeleteAccountAsync(string userId);
    }
}
