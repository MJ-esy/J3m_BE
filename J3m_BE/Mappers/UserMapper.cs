using J3M.Shared.DTOs.Users.ProfileDtos;
using J3m_BE.Models;

namespace J3m_BE.Mappers
{
    public static class UserMapper
    {
        public static UserProfileDto ToProfileDto(this AppUser user)
        {
            //Convert AppUser to UserProfileDto
            return new UserProfileDto()
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                DisplayName = user.DisplayName
            };
        }

        //Apply updates from UpdateProfileDto to AppUser
        public static void MapToEntity(this UpdateProfileDto dto, AppUser user)
        {
            if (!string.IsNullOrWhiteSpace(dto.UserName))
                user.UserName = dto.UserName.Trim();

            if (!string.IsNullOrWhiteSpace(dto.DisplayName))
                user.DisplayName = dto.DisplayName.Trim();

            if (!string.IsNullOrWhiteSpace(dto.Email))
                user.Email = dto.Email.Trim();

        }
    }
}
