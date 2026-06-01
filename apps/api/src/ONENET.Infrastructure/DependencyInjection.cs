// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Interfaces;
using ONENET.Infrastructure.Persistence;
using ONENET.Infrastructure.Persistence.Repositories;
using ONENET.Infrastructure.Services; // Add for AuditService

namespace ONENET.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>()); // AppDbContext implements IUnitOfWork
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IAuditService, AuditService>(); // Register AuditService

            // Assume a default implementation for ICurrentUser or it comes from WebAPI
            // If ICurrentUser needs a concrete implementation here for testing or specific scenarios
            // For now, assuming it's correctly provided by WebAPI layer or a common project.

            return services;
        }
    }
}