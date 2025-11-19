using System.ComponentModel.DataAnnotations;

namespace J3m_BE.DTOs.Users;


public class RegisterDto
{
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [MinLength(6)]
    [StringLength(100)]
    public string Password { get; set; } = string.Empty;

    [StringLength(50)]
    public string? DisplayName { get; set; } = string.Empty;

}
