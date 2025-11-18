using System.Text;
using J3m_BE.Data;
using J3m_BE.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace J3m_BE.Extensions;

public static class IdentityJwtExtension
{
    public static IServiceCollection AddIdentityAndJwt(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequiredLength = 8;
            opt.Password.RequireDigit = true;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireNonAlphanumeric = true;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        var jwt = config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

        // Ensure JwtBearer is the default authentication and challenge scheme.
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwt["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwt["Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdmin", p => p.RequireRole("Admin"));
        });

        return services;
    }

    public static async Task SeedRolesAsync(this IServiceProvider sp)
    {
        using var scope = sp.CreateScope();
        var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        string[] roles =new[] { "Admin", "User" };
        foreach (var r in roles)
            if (!await roleMgr.RoleExistsAsync(r))
                await roleMgr.CreateAsync(new IdentityRole(r));

        //Optional: seed an admin
           var adminEmail = "admin@example.com";
        var admin = await userMgr.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new AppUser { Email = adminEmail, UserName = "admin", DisplayName = "Admin" };
            await userMgr.CreateAsync(admin, "Admin!123");
            await userMgr.AddToRoleAsync(admin, "Admin");
        }
    }
}
