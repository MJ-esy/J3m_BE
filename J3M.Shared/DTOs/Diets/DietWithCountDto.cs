namespace J3m_BE.DTOs.Diets
{
    // DTO to transfer data about a diet and the number of recipes associated with it.
    //Computed property
    public class DietWithCountDto
    {
        public int DietId { get; set; }
        public string DietName { get; set; } = string.Empty;
        public int RecipeCount { get; set; }

    }
}
