using System.ComponentModel.DataAnnotations;

namespace J3m_BE.DTOs.Diets
{
    //DTO for creating a Diet
    public class DietCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string DietName { get; set; } = string.Empty;
    }
}
