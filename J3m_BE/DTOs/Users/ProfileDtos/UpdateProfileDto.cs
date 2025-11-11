using System.ComponentModel.DataAnnotations;

namespace J3m_BE.DTOs.Users;

public class UpdateProfileDto
{
    [StringLength(100)]
    public string? DisplayName { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }
}
