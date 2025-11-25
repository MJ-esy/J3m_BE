using J3M.Shared.MealPlanModels;
using J3m_BE.Data;
using J3m_BE.Extensions;
using J3m_BE.Middleware;
using J3m_BE.Services.Implementations;
using J3m_BE.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace J3m_BE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Db Context
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy => policy.WithOrigins("https://j3m.azurewebsites.net/", "https://localhost:7165/")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            //AI Services
            builder.Services.Configure<AzureOpenAiOptions>(builder.Configuration.GetSection("AzureOpenAI"));
            builder.Services.AddHttpClient<IAzureOpenAiService, AzureOpenAiService>(client =>
            {
                var endpoint = builder.Configuration["AzureOpenAI:Endpoint"];
                if (string.IsNullOrWhiteSpace(endpoint))
                    throw new InvalidOperationException("AzureOpenAI:Endpoint configuration is missing or empty.");
                client.BaseAddress = new Uri(endpoint);
            });

            // Identity + JWT
            builder.Services.AddIdentityAndJwt(builder.Configuration);

            // Add services to the container.
            builder.Services.AddJ3MCore();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "J3M_BE API",
                    Version = "v1"
                });

                // 1. Define Bearer-scheme
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter JWT token. Paste only the token; Swagger UI will add the 'Bearer ' prefix.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",

                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", jwtSecurityScheme);

                // 2.Ensures Swagger that all endpoints can use this scheme
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });


            var app = builder.Build();

            // Use CORS
            app.UseCors("AllowFrontend");


            //Runs async seeding
            app.Services.SeedRolesAsync().GetAwaiter().GetResult();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
