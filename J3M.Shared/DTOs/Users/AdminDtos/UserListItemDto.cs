using System.ComponentModel.DataAnnotations;

namespace J3M.Shared.DTOs.Users.AdminDtos;

public class UserListItemDto
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string UserName { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(254)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? DisplayName { get; set; }
}