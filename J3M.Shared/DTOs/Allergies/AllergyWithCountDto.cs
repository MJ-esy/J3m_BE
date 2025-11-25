namespace J3M.Shared.DTOs.Allergies
{
    // DTO to transfer data about a allergy and the number of ingredients associated with it.
    //Computed property
    public class AllergyWithCountDto
    {
        public int AllergyId { get; set; }
        public string AllergyName { get; set; } = string.Empty;
        public int IngredientCount { get; set; }
    }
}
