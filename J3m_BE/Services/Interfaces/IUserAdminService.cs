using J3m_BE.DTOs.Users;

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
