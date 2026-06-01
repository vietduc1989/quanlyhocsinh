<!-- QUAN-20260530-2301 -->
using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ONENET.Application;
using ONENET.Infrastructure;
using ONENET.WebAPI.Middleware;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace ONENET.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "ONENET.WebAPI")
                .WriteTo.Console()
                .WriteTo.File(new CompactJsonFormatter(), "logs/log-.json", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Starting ONENET.WebAPI host");
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog(); // Use Serilog for hosting logs

                // Add services to the container.
                builder.Services.AddHttpContextAccessor();
                builder.Services.AddApplication();
                builder.Services.AddInfrastructure(builder.Configuration);

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ONENET Student Management API", Version = "v1" });

                    // Configure Swagger to use JWT Bearer
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\"",
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

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
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
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                        };
                    });
                builder.Services.AddAuthorization(options =>
                {
                    // Define policies if needed, e.g., for specific permissions
                    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                    options.AddPolicy("TeacherPolicy", policy => policy.RequireRole("Admin", "Teacher"));
                });

                // Configure CORS
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        policy =>
                        {
                            policy.WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>())
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials(); // If you need to send cookies/auth headers
                        });
                });

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ONENET Student Management API V1");
                        c.RoutePrefix = "swagger"; // Access Swagger UI at /swagger
                    });
                    // Enable detailed error pages for development
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    // Global exception handling for production
                    app.UseGlobalExceptionMiddleware();
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseCors(); // Use CORS middleware
                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "ONENET.WebAPI terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}