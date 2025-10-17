using System.ComponentModel.DataAnnotations;

namespace J3m_BE.Models;

// Model representing an administrator user

public class Admin
{
    [Key]
    public int AdminId { get; set; }

    [Required, MaxLength(50)] 
    public string Username { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
}