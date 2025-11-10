using J3m_BE.DTOs.Users.AdminDtos;

namespace J3m_BE.Services.Interfaces
{
    public interface IUserAdminService
    {
        Task<string> CreateUserAsync(CreateUserByAdminDto dto);
        Task SetRolesAsync(string userId, IEnumerable<string> roles);
        Task DeleteUserAsync(string userId);

    }
}
