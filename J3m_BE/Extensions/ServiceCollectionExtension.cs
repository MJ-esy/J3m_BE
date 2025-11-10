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
        services.AddScoped<INutrientGroupRepository, NutrientGroupRepository>();

        services.AddScoped<IDietRepository, DietRepository>();

        // Add Services here
        services.AddScoped<IFoodGroupService, FoodGroupService>();
        services.AddScoped<INutrientGroupService, NutrientGroupService>();

        services.AddScoped<IDietService, DietService>();

 
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserAdminService, UserAdminService>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        // Add Configurations here


        return services;
    }
}