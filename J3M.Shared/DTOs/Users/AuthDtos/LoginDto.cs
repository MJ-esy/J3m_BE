using System.ComponentModel.DataAnnotations;

namespace J3M.Shared.DTOs.Users.AuthDtos;

public class LoginDto
{
    [Required]
    [StringLength(100)]
    public string EmailOrUserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}
