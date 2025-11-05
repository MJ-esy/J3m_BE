using System.ComponentModel.DataAnnotations;

namespace J3m_BE.DTOs.User
{
    public class UserCreateDto
    {
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
