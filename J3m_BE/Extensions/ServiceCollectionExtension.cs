using J3m_BE.Repositories.Implementations;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services;
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
        services.AddScoped<IIngredientRepository, IngredientRepository>();
        
        // Add Services here
        services.AddScoped<IFoodGroupService, FoodGroupService>();
        services.AddScoped<IIngredientService, IngredientService>();
        
        // Add Configurations here
        
        
        return services;
    }
}