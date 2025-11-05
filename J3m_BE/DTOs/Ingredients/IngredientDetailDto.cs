namespace J3m_BE.DTOs.Ingredients;

// DTO for detailed ingredient information
// Used for fetching a single ingredient with all related data (detailed view)

public class IngredientDetailDto
{
    public int IngredientId { get; set; }
    public string IngredientName { get; set; } = string.Empty;
    public string? FoodGroupName { get; set; }
    public IEnumerable<string> Allergies { get; set; } = [];
    public IEnumerable<string> NutrientGroups { get; set; } = [];
}