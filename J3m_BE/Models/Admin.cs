using System.ComponentModel.DataAnnotations;

namespace J3m_BE.Models;

// Model representing an administrator user

public class Admin
{
    [Key]
    public int AdminId { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string PasswordHash { get; set; }
}