using J3m_BE.Models;
using J3m_BE.Models.Links;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Data;

// Database context class
// Inherits from DbContext and configures entity relationships
// and composite keys for link entities

// WIP: Add more relationship configurations as needed

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  // DbSets for each entity
  public DbSet<User> Users => Set<User>();
  public DbSet<Admin> Admins => Set<Admin>();
  public DbSet<Recipe> Recipes => Set<Recipe>();
  public DbSet<Ingredient> Ingredients => Set<Ingredient>();
  public DbSet<FoodGroup> FoodGroups => Set<FoodGroup>();
  public DbSet<NutrientGroup> NutrientGroups => Set<NutrientGroup>();
  public DbSet<Allergy> Allergies => Set<Allergy>();
  public DbSet<Diet> Diets => Set<Diet>();

  // DbSets for link entities
  public DbSet<UserRecipe> UserRecipes => Set<UserRecipe>();
  public DbSet<DietRecipe> DietRecipes => Set<DietRecipe>();
  public DbSet<IngredientRecipe> IngredientRecipes => Set<IngredientRecipe>();
  public DbSet<IngredientAllergy> IngredientAllergies => Set<IngredientAllergy>();
  public DbSet<IngredientNutrientGroup> IngredientNutrientGroups => Set<IngredientNutrientGroup>();

  protected override void OnModelCreating(ModelBuilder b)
  {
    base.OnModelCreating(b);

    // Composite keys
    b.Entity<UserRecipe>().HasKey(x => new { x.UserId, x.RecipeId });
    b.Entity<DietRecipe>().HasKey(x => new { x.DietId, x.RecipeId });
    b.Entity<IngredientRecipe>().HasKey(x => new { x.IngredientId, x.RecipeId });
    b.Entity<IngredientAllergy>().HasKey(x => new { x.IngredientId, x.AllergyId });
    b.Entity<IngredientNutrientGroup>().HasKey(x => new { x.IngredientId, x.NutrientGroupId });

    // Relationships
    b.Entity<Ingredient>()
        .HasOne(i => i.FoodGroup)
        .WithMany(f => f.Ingredients)
        .HasForeignKey(i => i.FoodGroupId);
    
    // Cascade delete behavior for Recipe -> Link Entities
    b.Entity<UserRecipe>()
        .HasOne(ur => ur.Recipe)
        .WithMany(r => r.UserLinks)
        .HasForeignKey(ur => ur.RecipeId)
        .OnDelete(DeleteBehavior.Cascade);
    
    b.Entity<DietRecipe>()
        .HasOne(dr => dr.Recipe)
        .WithMany(r => r.DietLinks)
        .HasForeignKey(dr => dr.RecipeId)
        .OnDelete(DeleteBehavior.Cascade);
    
    b.Entity<IngredientRecipe>()
        .HasOne(ir => ir.Recipe)
        .WithMany(r => r.IngredientLinks)
        .HasForeignKey(ir => ir.RecipeId)
        .OnDelete(DeleteBehavior.Cascade);
    
    // NO Cascade delete for Link Entities -> Parent Entities
    b.Entity<UserRecipe>()
        .HasOne(ur => ur.User)
        .WithMany(u => u.UserRecipes)
        .HasForeignKey(ur => ur.UserId)
        .OnDelete(DeleteBehavior.Restrict);
    
    b.Entity<DietRecipe>()
        .HasOne(dr => dr.Diet)
        .WithMany(d => d.RecipeLinks)
        .HasForeignKey(dr => dr.DietId)
        .OnDelete(DeleteBehavior.Restrict);
    
    b.Entity<IngredientRecipe>()
        .HasOne(ir => ir.Ingredient)
        .WithMany(i => i.RecipeLinks)
        .HasForeignKey(ir => ir.IngredientId)
        .OnDelete(DeleteBehavior.Restrict);
    

    //Data Seeding
    b.SeedAll();
  }


}