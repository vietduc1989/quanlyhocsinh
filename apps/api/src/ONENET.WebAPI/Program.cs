// QUAN-20260530-2301
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ONENET.Application;
using ONENET.Infrastructure;
using ONENET.WebAPI.Middleware;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor(); // Required for ICurrentUser

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.WithCorrelationIdHeader() // Enriches logs with Correlation-ID header
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key not configured")))
        };
    });

builder.Services.AddAuthorization(options =>
{
    // Example policy for Admin role
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    // Example policy for Teacher role
    options.AddPolicy("TeacherPolicy", policy => policy.RequireRole("Teacher"));
    // A policy that allows both Admin and Teacher (for GET operations)
    options.AddPolicy("AdminOrTeacherPolicy", policy => policy.RequireRole("Admin", "Teacher"));
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins(
            "http://localhost:3000", // Frontend development URL
            "https://yourfrontend.com") // Production frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()); // Allow credentials for JWT
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(); // Add Serilog request logging

// Custom Exception Handling Middleware must be first
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting(); // UseRouting must come before UseCors and UseAuthentication/Authorization

app.UseCors("AllowSpecificOrigin"); // Apply CORS policy

app.UseAuthentication();
app.UseAuthorization();

// Custom CurrentUserMiddleware (optional, if ICurrentUser needs explicit HttpContext.User population)
// If ICurrentUser's implementation relies directly on IHttpContextAccessor and it's configured correctly,
// this middleware might not be strictly necessary, but serves as a conceptual point.
// app.UseMiddleware<CurrentUserMiddleware>();

app.MapControllers();

app.Run();