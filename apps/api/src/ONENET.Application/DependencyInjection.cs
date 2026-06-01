// QUAN-20260530-2302
// Assume DependencyInjection.cs already exists, adding registrations.
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ONENET.Application.Classes.Commands;
using ONENET.Application.Classes.Queries;
using ONENET.Application.Classes.Validators;
using ONENET.Application.Common.Behaviors;`r`n// QUAN-20260531-154643
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            });

            // Register specific validators and handlers if not picked up by assembly scan
            // (Assembly.GetExecutingAssembly() should pick these up, but explicit adds for clarity/safety)

            // Queries
            services.AddTransient<IRequestHandler<GetClassesQuery, PaginatedList<ClassDto>>, GetClassesQueryHandler>();
            services.AddTransient<IRequestHandler<GetClassByIdQuery, ClassDto>, GetClassByIdQueryHandler>();
            services.AddTransient<IValidator<GetClassesQuery>, GetClassesQueryValidator>();

            // Commands
            services.AddTransient<IRequestHandler<CreateClassCommand, ClassIdDto>, CreateClassCommandHandler>();
            services.AddTransient<IValidator<CreateClassCommand>, CreateClassCommandValidator>();
            services.AddTransient<IRequestHandler<UpdateClassCommand>, UpdateClassCommandHandler>();
            services.AddTransient<IValidator<UpdateClassCommand>, UpdateClassCommandValidator>();
            services.AddTransient<IRequestHandler<DeleteClassCommand>, DeleteClassCommandHandler>();`r`n            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
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