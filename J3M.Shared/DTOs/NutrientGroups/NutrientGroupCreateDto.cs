using System.ComponentModel.DataAnnotations;

namespace J3M.Shared.DTOs.NutrientGroups
{
    public class NutrientGroupCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string NutrientGroupName { get; set; } = string.Empty;
    }
}
