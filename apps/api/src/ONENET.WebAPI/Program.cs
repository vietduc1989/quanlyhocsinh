// QUAN-20260531-154643
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ONENET.Application; // Add application services
using ONENET.Infrastructure; // Add infrastructure services
using ONENET.WebAPI.Middleware; // For global exception middleware
using Microsoft.OpenApi.Models; // For Swagger Authorization`r`nusing Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ONENET.Application;
using ONENET.Application.Common.Interfaces;
using ONENET.Infrastructure;
using ONENET.WebAPI.Services; // Assuming CurrentUserService implementation
using Serilog;
using System.Text;
using ONENET.WebAPI.Middleware; // For GlobalExceptionMiddleware

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());`r`nbuilder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddControllers();

// Add Application and Infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Add API Explorer and Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ONENET Scores API", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\n\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\"",`r`n// Custom services
builder.Services.AddSingleton<ICurrentUser, CurrentUserService>(); // Assuming CurrentUserService implements ICurrentUser

// Add Clean Architecture layers
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ONENET API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

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
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
        };
    });

// Configure Authorization (Policy-based can be added here if needed, beyond role-based)
builder.Services.AddAuthorization(options =>
{
    // Example policy for Admin role
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    // Example policy for Teacher role
    options.AddPolicy("TeacherOnly", policy => policy.RequireRole("Teacher"));
    // A combined policy could be useful if specific endpoints need either
    options.AddPolicy("AdminOrTeacher", policy => policy.RequireRole("Admin", "Teacher"));
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(builder.Configuration["CorsSettings:AllowedOrigins"]?.Split(';') ?? Array.Empty<string>())
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // If using cookies/auth headers with specific origins
    });`r`n            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is not configured.")))
        };
    });

// Configure Authorization Policies for roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CanManageSubjects", policy => policy.RequireRole("Admin", "ChuyenVienDaoTao"));
    options.AddPolicy("CanViewSubjects", policy => policy.RequireRole("Admin", "ChuyenVienDaoTao", "GiaoVien"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(builder.Configuration["Cors:AllowedOrigins"]?.Split(',') ?? Array.Empty<string>())
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(); // Add Serilog for request logging

app.UseHttpsRedirection();

app.UseCors(); // Use CORS middleware

app.UseMiddleware<GlobalExceptionMiddleware>(); // Use global exception handler

app.UseAuthentication(); // Must be before Authorization
app.UseAuthorization();`r`napp.UseSerilogRequestLogging(); // Add Serilog request logging

app.UseMiddleware<GlobalExceptionMiddleware>(); // Register custom global exception handler

app.UseHttpsRedirection();

app.UseRouting(); // Important: UseRouting before UseCors, UseAuthentication, UseAuthorization

app.UseCors(); // Apply CORS policy

app.UseAuthentication(); // JWT Authentication
app.UseAuthorization(); // Role-based Authorization

app.MapControllers();

app.Run();