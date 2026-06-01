using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Interfaces;
using ONENET.Infrastructure.Persistence;
using ONENET.Infrastructure.Persistence.Repositories;
using ONENET.Infrastructure.Services;

namespace ONENET.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Generic repository
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ILopRepository, LopRepository>();
            services.AddScoped<ITrangThaiHocSinhRepository, TrangThaiHocSinhRepository>();

            services.AddTransient<ICurrentUser, CurrentUser>(); // Current user service

            return services;
        }
    }
}
