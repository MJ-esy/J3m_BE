using System.ComponentModel.DataAnnotations;

namespace J3M.Shared.DTOs.Admin
{
    public class AdminRegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
