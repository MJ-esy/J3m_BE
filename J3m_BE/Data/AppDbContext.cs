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
            .HasForeignKey(i => i.FoodGroupId)
            .OnDelete(DeleteBehavior.SetNull);
        
        b.Entity<UserRecipe>()
            .HasOne(u => u.User)
            .WithMany(ur => ur.UserRecipes)
            .HasForeignKey(u => u.UserId);
        
        b.Entity<UserRecipe>()
            .HasOne(r => r.Recipe)
            .WithMany(ur => ur.UserLinks)
            .HasForeignKey(r => r.RecipeId);
    }
}