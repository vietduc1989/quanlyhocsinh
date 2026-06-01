// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Common;
using ONENET.Domain.Entities;
using System.Reflection;

namespace ONENET.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        private readonly ICurrentUser _currentUser;

        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUser currentUser)
            : base(options)
        {
            _currentUser = currentUser;
        }

        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Global Query Filter for soft delete as per guideline
            builder.Entity<Subject>().HasQueryFilter(s => !s.IsDeleted);

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUser.UserName ?? "system_user";
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.IsDeleted = false;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUser.UserName ?? "system_user";
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}