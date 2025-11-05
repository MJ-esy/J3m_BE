using J3m_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Data;

//Adding data to Database when migrating
public static class SeedData
{
  public static void SeedDiet(this ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Diet>().HasData(
      new Diet
      {
        DietId = 1,
        DietName = "Keto"
      },
      new Diet
      {
        DietId = 2,
        DietName = "Paleo"
      },
      new Diet
      {
        DietId = 3,
        DietName = "Pescetarian"
      },
      new Diet
      {
        DietId = 4,
        DietName = "Vegan"
      },
      new Diet
      {
        DietId = 5,
        DietName = "Vegetarian"
      });
  }

  public static void SeedFoodGroup(this ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<FoodGroup>().HasData(
      new FoodGroup
      {
        FoodGroupId = 1,
        FoodGroupName = "Fruits"
      },
      new FoodGroup
      {
        FoodGroupId = 2,
        FoodGroupName = "Vegetables"
      },
      new FoodGroup
      {
        FoodGroupId = 3,
        FoodGroupName = "Grains"
      },
      new FoodGroup
      {
        FoodGroupId = 4,
        FoodGroupName = "Meats/Egg"
      },
      new FoodGroup
      {
        FoodGroupId = 5,
        FoodGroupName = "Dairy"
      },
      new FoodGroup
      {
        FoodGroupId = 6,
        FoodGroupName = "Nuts/Seed"
      },
      new FoodGroup
      {
        FoodGroupId = 7,
        FoodGroupName = "Legume"
      });
  }
  
  public static void SeedNutrientGroup(this ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<NutrientGroup>().HasData(
      new NutrientGroup
      {
        NutrientGroupId = 1,
        NutrientGroupName = "Carbohydrates"
      },
      new NutrientGroup
      {
        NutrientGroupId = 2,
        NutrientGroupName = "Protein"
      },
      new NutrientGroup
      {
        NutrientGroupId = 3,
        NutrientGroupName = "Fats and oils"
      },
      new NutrientGroup
      {
        NutrientGroupId = 4,
        NutrientGroupName = "Dietary fibre"
      });
  }
  
  public static void SeedAllergies(this ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Allergy>().HasData(
      new Allergy
      {
        AllergyId = 1,
        AllergyName = "Gluten"
      },
      new Allergy
      {
        AllergyId = 2,
        AllergyName = "Diary(milk protein)"
      },
      new Allergy
      {
        AllergyId = 3,
        AllergyName = "Lactose intolerance"
      },
      new Allergy
      {
        AllergyId = 4,
        AllergyName = "Wheat"
      },
      new Allergy
      {
        AllergyId = 5,
        AllergyName = "Egg"
      },
      new Allergy
      {
        AllergyId = 6,
        AllergyName = "Shellfish"
      },
      new Allergy
      {
        AllergyId = 7,
        AllergyName = "Fish"
      },
      new Allergy
      {
        AllergyId = 8,
        AllergyName = "Nuts"
      },
      new Allergy
      {
        AllergyId = 9,
        AllergyName = "Peanuts"
      },
      new Allergy
      {
        AllergyId = 10,
        AllergyName = "Celery"
      });
  }

  public static void SeedAll(this ModelBuilder modelBuilder)
  {
    modelBuilder.SeedDiet();
    modelBuilder.SeedFoodGroup();
    modelBuilder.SeedNutrientGroup();
    modelBuilder.SeedAllergies();
  }
}