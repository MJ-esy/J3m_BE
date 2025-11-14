using System.ComponentModel.DataAnnotations;

namespace J3m_BE.DTOs.NutrientGroups
{
    public class CreateNutrientGroupDto
    {
        [Required]
        [MaxLength(100)]
        public string NutrientGroupName { get; set; } = string.Empty;
    }
}
