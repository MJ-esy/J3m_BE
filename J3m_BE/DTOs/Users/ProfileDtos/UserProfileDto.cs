using System.ComponentModel.DataAnnotations;

namespace J3m_BE.DTOs.Users.ProfileDtos;

public class UserProfileDto
{
    public string Id { get; set; } = string.Empty;

    [StringLength(100)]
    public string? DisplayName { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [StringLength(50)]
    public string UserName { get; set; } = string.Empty;
}
