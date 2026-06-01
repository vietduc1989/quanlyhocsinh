// QUAN-20260530-2302
// Assume DependencyInjection.cs already exists, adding registrations.`r`n// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Features.Scores.Queries;
using ONENET.Domain.Interfaces;
using ONENET.Infrastructure.Persistence;
using ONENET.Infrastructure.Persistence.Repositories;
using ONENET.Infrastructure.Services; // Example, assuming some services`r`nusing ONENET.Infrastructure.Services;
using ONENET.Application.Features.Scores.Services; // For IScorePermissionService
using ONENET.Application; // For IUnitOfWork`r`nusing ONENET.Infrastructure.Services; // Add for AuditService

namespace ONENET.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure PostgreSQL DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                    .UseSnakeCaseNamingConvention()); // For PostgreSQL snake_case

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());

            // Register Repositories
            services.AddScoped<IClassRepository, ClassRepository>();
            // Add other repositories here

            // Register other infrastructure services (e.g., IDateTime, IEmailSender)
            services.AddTransient<IDateTime, DateTimeService>(); // Example service`r`n            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))); // For migrations

            // Register IAppDbContext as an interface to AppDbContext
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());

            // Register repositories
            services.AddScoped<IScoreRepository, ScoreRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            // Register external entity repositories
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();

            // Register common services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            // Register domain/application services
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped<IScorePermissionService, ScorePermissionService>(); // Concrete implementation`r`n                options.UseNpgsql(connectionString,
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