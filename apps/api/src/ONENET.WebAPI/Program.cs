// QUAN-20260530-2301
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ONENET.Application;
using ONENET.Infrastructure;
using ONENET.WebAPI.Middleware;
using ONENET.Application.Common.Interfaces; // For ICurrentUser

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console());

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Register Clean Architecture layers
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    // Register ICurrentUser service (example using HttpContextAccessor)
    builder.Services.AddHttpContextAccessor();
    // Assuming a concrete implementation of ICurrentUser exists that uses HttpContextAccessor
    // For this example, we might need a basic CurrentUserService:
    builder.Services.AddScoped<ICurrentUser, CurrentUserService>();


    var app = builder.Build();

    // Configure the HTTP request pipeline.

    // Add Global Exception Middleware at the very top
    app.UseMiddleware<GlobalExceptionMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // Authentication and Authorization
    app.UseAuthentication(); // This should come before UseAuthorization
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

// Minimal CurrentUserService for demonstration, should be in Infrastructure/Services/CurrentUserService.cs
namespace ONENET.Infrastructure.Services
{
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;
    using ONENET.Application.Common.Interfaces;

    public class CurrentUserService : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public bool IsInRole(string roleName)
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole(roleName) ?? false;
        }
    }
}