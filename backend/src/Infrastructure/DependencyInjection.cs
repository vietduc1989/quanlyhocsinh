using Application.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure DbContext to use an in-memory database for development simplicity
        // For production, replace with SQL Server, PostgreSQL, etc.
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("CleanArchitectureDb")); // Or options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}