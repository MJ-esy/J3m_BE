using J3m_BE.DTOs.Users.ProfileDtos;
using J3m_BE.Models;

namespace J3m_BE.Mappers
{
    public static class UserMapper
    {
        public static ProfileDto ToProfileDto(this AppUser user)
        {
            return new ProfileDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email ?? string.Empty,
                UserName = user.UserName ?? string.Empty
            };
        }   


        public static void MapToEntity(this UpdateProfileDto dto, AppUser user)
        {
            if (!string.IsNullOrWhiteSpace(dto.DisplayName))
            
                user.DisplayName = dto.DisplayName.Trim();

            if (!string.IsNullOrWhiteSpace(dto.Email))

                user.Email = dto.Email.Trim();
            
        }
    }
}
