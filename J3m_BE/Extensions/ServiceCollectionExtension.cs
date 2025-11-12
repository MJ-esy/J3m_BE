using J3m_BE.Repositories;
using J3m_BE.Repositories.Implementations;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services;
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

        // Add Services here
        services.AddScoped<IFoodGroupService, FoodGroupService>();
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<INutrientGroupService, NutrientGroupService>();
        services.AddScoped<IAllergyService, AllergyService>();
        services.AddScoped<IDietService, DietService>();
        
        // Add Configurations here


        return services;
    }
}