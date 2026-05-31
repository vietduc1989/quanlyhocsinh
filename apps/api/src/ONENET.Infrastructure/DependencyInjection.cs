// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Interfaces;
using ONENET.Infrastructure.Persistence;
using ONENET.Infrastructure.Persistence.Repositories;
using ONENET.Infrastructure.Services; // Assuming CurrentUserService might be here

namespace ONENET.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());

            // Register repositories
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ILopRepository, LopRepository>();
            services.AddScoped<ITrangThaiHocSinhRepository, TrangThaiHocSinhRepository>();

            // Register ICurrentUser service (assuming implementation in Infrastructure/Services)
            // If ICurrentUser is already registered elsewhere (e.g., WebAPI for HttpContext access),
            // this line might not be needed or would be a specific implementation.
            // For this task, we will assume a basic HttpContextAccessor-based implementation
            // exists in Infrastructure/Services or is provided by the WebAPI layer.
            // For now, let's just make sure it's accessible.
            // services.AddScoped<ICurrentUser, CurrentUserService>(); // Example if CurrentUserService is in Infrastructure

            return services;
        }
    }
}