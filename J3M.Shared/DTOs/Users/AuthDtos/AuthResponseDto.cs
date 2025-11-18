namespace J3m_BE.DTOs.Users;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
}
