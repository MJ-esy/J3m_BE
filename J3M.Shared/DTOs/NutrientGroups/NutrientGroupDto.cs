namespace J3m_BE.DTOs.NutrientGroups
{
  public class NutrientGroupDto
  {
    public int NutrientGroupId { get; set; }
    public string NutrientGroupName { get; set; } = string.Empty;
    public int IngredientCount { get; set; }
  }
}
