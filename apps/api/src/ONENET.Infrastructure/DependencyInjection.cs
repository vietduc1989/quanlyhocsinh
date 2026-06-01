// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Features.Scores.Queries;
using ONENET.Domain.Interfaces;
using ONENET.Infrastructure.Persistence;
using ONENET.Infrastructure.Persistence.Repositories;
using ONENET.Infrastructure.Services;
using ONENET.Application.Features.Scores.Services; // For IScorePermissionService
using ONENET.Application; // For IUnitOfWork

namespace ONENET.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
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
            services.AddScoped<IScorePermissionService, ScorePermissionService>(); // Concrete implementation

            return services;
        }
    }
}