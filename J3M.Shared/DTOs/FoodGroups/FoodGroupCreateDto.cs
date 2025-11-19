using System.ComponentModel.DataAnnotations;

namespace J3m_BE.DTOs.FoodGroups;

// DTO for creating a food group

public class FoodGroupCreateDto
{
    [Required, MaxLength(100)]
    public string FoodGroupName { get; set; } = string.Empty;
}