using J3M.Shared.DTOs.Users.AdminDtos;

namespace J3m_BE.Services.Interfaces
{
    public interface IUserAdminService
    {
        Task<string> CreateUserAsync(CreateUserByAdminDto dto);
        Task SetRolesAsync(string userId, IEnumerable<string> roles);
        Task DeleteUserAsync(string userId);
        Task<IEnumerable<UserListItemDto>> GetAllAsync();
    }
}
