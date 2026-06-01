// QUAN-20260530-2302
// Assume AppDbContext.cs already exists, adding DbSet<Class>
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUser _currentUser; // For BaseAuditableEntity

        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUser currentUser)
            : base(options)
        {
            _currentUser = currentUser;
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; } // Assume exists
        public DbSet<Teacher> Teachers { get; set; } // Assume exists

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _currentUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = _currentUser.UserId;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; // Perform soft delete
                        entry.Entity.IsDeleted = true;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = _currentUser.UserId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Add global query filter for soft delete
            modelBuilder.Entity<Class>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<Teacher>().HasQueryFilter(t => !t.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}