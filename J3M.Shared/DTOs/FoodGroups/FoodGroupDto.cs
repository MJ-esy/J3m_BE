namespace J3m_BE.DTOs.FoodGroups;

// DTO for transferring food group data

public class FoodGroupDto
{
    public int FoodGroupId { get; set; }
    public string FoodGroupName { get; set; } = string.Empty;
    public int IngredientCount { get; set; } // Computed property for number of ingredients
}