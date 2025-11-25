using System.ComponentModel.DataAnnotations;

namespace J3M.Shared.DTOs.FoodGroups;

// DTO for creating a food group

public class FoodGroupCreateDto
{
    [Required, MaxLength(100)]
    public string FoodGroupName { get; set; } = string.Empty;
}