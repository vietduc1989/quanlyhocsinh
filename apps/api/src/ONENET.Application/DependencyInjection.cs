// QUAN-20260531-154643
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ONENET.Application.Common.Behaviors; // Assuming ValidationBehavior and LoggingBehavior exist
using ONENET.Application.Features.Scores.Services;`r`nusing ONENET.Application.Common.Behaviors; // Assuming these exist from guideline

namespace ONENET.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); // If using AutoMapper
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); // Add logging behavior

            // Register Score-related services
            services.AddScoped<IScorePermissionService, ScorePermissionService>();`r`n            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); // Assuming LoggingBehavior exists
            });

            return services;
        }
    }

    // Placeholder for IUnitOfWork if not already defined globally in Common/Interfaces
    // This is required by command handlers
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}