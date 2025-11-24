using System.ComponentModel.DataAnnotations;

namespace J3M.Shared.DTOs.Users;

public class SetRolesDto
{
    // List of roles to assign to the user. MinLength(1) ensures there’s at least one role in the list
    [Required]
    [MinLength(1)]
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}
