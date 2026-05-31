using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;
using System.Reflection;

namespace ONENET.Infrastructure.Persistence;

/// <summary>
/// DbContext chính của hệ thống ONENET tích hợp Entity Framework Core trên PostgreSQL.
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Class> Classes => Set<Class>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tự động load tất cả các Configurations từ assembly hiện tại
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}