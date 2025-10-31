
using J3m_BE.Data;
using J3m_BE.Extensions;
using J3m_BE.Middleware;
using Microsoft.EntityFrameworkCore;

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

            // Add services to the container.
            builder.Services.AddJ3MCore();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseMiddleware<ErrorHandlingMiddleware>();
            
            app.UseHttpsRedirection();

            // app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();
            app.Run();
        }
    }
}
