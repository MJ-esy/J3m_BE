using J3M.Shared.DTOs.Allergies;
using J3M.Shared.DTOs.Diets;
using J3M.Shared.MealPlanModels;
using J3m_BE.Repositories;
using J3m_BE.Repositories.Implementations;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services;
using J3m_BE.Services.Common;
using J3m_BE.Services.Implementations;
using J3m_BE.Services.Interfaces;

namespace J3m_BE.Extensions;

// -- ----------------------------------------------------------------------------
// -- Project: J3m_BE
// -- Filename: ServiceCollectionExtension.cs
// -- Description: Extension method to add core services, repositories,
// --              and configurations to the IServiceCollection injection container.

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddJ3MCore(this IServiceCollection services)
    {
        // Add Repositories here
        services.AddScoped<IFoodGroupRepository, FoodGroupRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();        
        services.AddScoped<INutrientGroupRepository, NutrientGroupRepository>();
        services.AddScoped<IAllergyRepository, AllergyRepository>();
        services.AddScoped<IDietRepository, DietRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();

        // Add Services here
        services.AddScoped<IFoodGroupService, FoodGroupService>();
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<INutrientGroupService, NutrientGroupService>();
        services.AddScoped<IAllergyService, AllergyService>();
        services.AddScoped<IDietService, DietService>();
        services.AddScoped<IIngredientService, IngredientService>();
       
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserAdminService, UserAdminService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<IMealPlanService, MealPlanService>();
        services.AddScoped<IAzureOpenAiService, AzureOpenAiService>();

      

        // Add Configurations here

        return services;
    }
}