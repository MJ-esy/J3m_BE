using System.ComponentModel.DataAnnotations;

namespace J3m_BE.DTOs.Allergies
{
    public class AllergyCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string AllergyName { get; set; } = string.Empty;
    }
}
