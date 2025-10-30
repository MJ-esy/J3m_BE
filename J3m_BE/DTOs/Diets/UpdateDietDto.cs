using System.ComponentModel.DataAnnotations;

namespace J3m_BE.DTOs.Diets
{
    //DTO for updating existing Diet
    public class UpdateDietDto
    {
        [Required]
        [MaxLength(100)]
        public string DietName { get; set; } = string.Empty;
    }
}
