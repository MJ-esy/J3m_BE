using System.ComponentModel.DataAnnotations;

namespace J3M.Shared.DTOs.Diets
{
    //DTO for creating a Diet
    public class DietCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string DietName { get; set; } = string.Empty;
    }
}
