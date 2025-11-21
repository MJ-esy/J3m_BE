using System.ComponentModel.DataAnnotations;

namespace J3M.Shared.DTOs.Admin
{
    public class AdminLoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
