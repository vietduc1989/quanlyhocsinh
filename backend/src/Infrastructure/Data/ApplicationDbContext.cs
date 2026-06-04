using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ExampleItem> ExampleItems => Set<ExampleItem>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Here you could add logic for auditing (e.g., setting LastModifiedAt)
        return await base.SaveChangesAsync(cancellationToken);
    }
}