using System.ComponentModel.DataAnnotations;

namespace J3m_BE.DTOs.Users.ProfileDtos;

public class UpdateProfileDto
{
    [Required, MaxLength(100)]
    public string? UserName { get; set; } = string.Empty;

   
    [MaxLength(100)]
    public string? DisplayName { get; set; }

    [EmailAddress]
    [Required,MaxLength(256)]
    public string? Email { get; set; } = string.Empty;
}
