// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Interfaces;
using ONENET.Infrastructure.Persistence;
using ONENET.Infrastructure.Persistence.Interceptors;
using ONENET.Infrastructure.Persistence.Repositories;
using ONENET.Infrastructure.Services; // Assuming CurrentUserService is here

namespace ONENET.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("DefaultConnection string is not configured.");
        }

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());

        // Register repositories
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ILopRepository, LopRepository>();
        services.AddScoped<ITrangThaiHocSinhRepository, TrangThaiHocSinhRepository>();

        // Register CurrentUser service (mock for now, should integrate with Auth)
        services.AddScoped<ICurrentUser, CurrentUserService>();

        return services;
    }
}