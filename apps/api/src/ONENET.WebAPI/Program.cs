// QUAN-20260531-154643
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddControllers();

// Custom services
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
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
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

app.UseSerilogRequestLogging(); // Add Serilog request logging

app.UseMiddleware<GlobalExceptionMiddleware>(); // Register custom global exception handler

app.UseHttpsRedirection();

app.UseRouting(); // Important: UseRouting before UseCors, UseAuthentication, UseAuthorization

app.UseCors(); // Apply CORS policy

app.UseAuthentication(); // JWT Authentication
app.UseAuthorization(); // Role-based Authorization

app.MapControllers();

app.Run();