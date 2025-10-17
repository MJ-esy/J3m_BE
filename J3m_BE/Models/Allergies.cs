using System.ComponentModel.DataAnnotations;

namespace J3m_BE.Models;

public class Allergies
{
  [Key]
  public int AllergyId { get; set; }

  [Required, MaxLength(50)]
  public string AllergyName { get; set; } = string.Empty;

}