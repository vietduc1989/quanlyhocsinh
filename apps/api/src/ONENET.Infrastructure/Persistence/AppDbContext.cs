// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Common;
using ONENET.Domain.Entities;
using ONENET.Infrastructure.Persistence.Interceptors;
using System.Reflection;

namespace ONENET.Infrastructure.Persistence;

public class AppDbContext : DbContext, IUnitOfWork
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Student> Students { get; set; } = default!;
    public DbSet<Lop> Lops { get; set; } = default!;
    public DbSet<TrangThaiHocSinh> TrangThaiHocSinhs { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure Global Query Filters for soft delete
        builder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);
        builder.Entity<Lop>().HasQueryFilter(l => !l.IsDeleted);
        builder.Entity<TrangThaiHocSinh>().HasQueryFilter(t => !t.IsDeleted);

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}