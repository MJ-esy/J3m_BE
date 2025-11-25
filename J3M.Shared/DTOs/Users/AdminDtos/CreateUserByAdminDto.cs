using System.ComponentModel.DataAnnotations;

namespace J3M.Shared.DTOs.Users.AdminDtos;

// DTO for admin to create a new user with roles. MinLength(1) ensures there’s at least one role in the list
public class CreateUserByAdminDto
{
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = "Temp!123";

    [StringLength(100)]
    public string? DisplayName { get; set; }

    [MinLength(1, ErrorMessage = "At least one role must be specified.")]
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}
