using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Extensions;

// -- ----------------------------------------------------------------------------
// -- Project: J3m_BE
// -- Filename: ServiceCollectionExtension.cs
// -- Description: Extension method to add core services, repositories,
// --              and configurations to the IServiceCollection injection container.

public static class ServiceCollectionExtension
{
  public static IServiceCollection AddJ3MCore(this IServiceCollection services, IConfiguration configuration)
  {
    // Add Repositories here
    services.AddDbContext<Data.AppDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    // Add Services here

    // Add Configurations here

    return services;
  }
}