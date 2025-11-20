using System.ComponentModel.DataAnnotations;

namespace J3m_BE.DTOs.Users.ProfileDtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;
        
        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; } = string.Empty;
        [Required]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; } = string.Empty;

    }
}
