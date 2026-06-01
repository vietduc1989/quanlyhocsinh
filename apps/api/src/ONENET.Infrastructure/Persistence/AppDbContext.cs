// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces; // For IAppDbContext
using ONENET.Domain.Entities;
using ONENET.Domain.Common; // For BaseEntity`r`nusing System.Reflection;

namespace ONENET.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext, IUnitOfWork
    {
        private readonly ICurrentUser _currentUser; // To populate CreatedBy/UpdatedBy

        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUser currentUser)
            : base(options)
        {
            _currentUser = currentUser;
        }

        public DbSet<Score> Scores => Set<Score>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Semester> Semesters => Set<Semester>();`r`n        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Configure global query filter for soft delete for BaseEntity inheritors
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(e => !((BaseEntity)e).IsDeleted);
                }
            }

            base.OnModelCreating(modelBuilder);`r`n            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

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
                        entry.Entity.CreatedBy = _currentUser.UserName ?? "System";`r`n                        entry.Entity.CreatedBy = _currentUser.UserName ?? "system_user";
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.IsDeleted = false;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUser.UserName ?? "System";
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        // For soft delete, IsDeleted is explicitly set in entity/command handler
                        break;
                    case EntityState.Deleted:
                        // This case should be rare due to soft delete.
                        // If a hard delete is performed, it bypasses soft delete, but our commands use soft delete.
                        if (!entry.Entity.IsDeleted)
                        {
                            entry.State = EntityState.Modified;
                            entry.Entity.IsDeleted = true;
                            entry.Entity.UpdatedBy = _currentUser.UserName ?? "System";
                            entry.Entity.UpdatedAt = DateTime.UtcNow;
                        }
                        break;
                }
            }
`r`n                        entry.Entity.UpdatedBy = _currentUser.UserName ?? "system_user";
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}