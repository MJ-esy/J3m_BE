using J3m_BE.DTOs.Users;
using J3m_BE.DTOs.Users.AuthDtos;


namespace J3m_BE.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}