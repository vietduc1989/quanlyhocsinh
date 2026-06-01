<!-- QUAN-20260530-2301 -->
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Common;
using ONENET.Domain.Entities;

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

        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Lop> Lops { get; set; } = default!;
        public DbSet<TrangThaiHocSinh> TrangThaiHocSinhs { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Global Query Filter for soft delete
            modelBuilder.Entity<Student>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Lop>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<TrangThaiHocSinh>().HasQueryFilter(e => !e.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _currentUser.UserName;
                        entry.Entity.UpdatedAt = DateTime.UtcNow; // Set initial UpdatedAt on creation
                        entry.Entity.UpdatedBy = _currentUser.UserName; // Set initial UpdatedBy on creation
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = _currentUser.UserName;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; // Soft delete
                        entry.Entity.IsDeleted = true;
                        entry.Entity.UpdatedAt = DateTime.UtcNow; // Update on soft delete
                        entry.Entity.UpdatedBy = _currentUser.UserName; // Update on soft delete
                        break;
                }
            }
            return await base.SaveChangesAsync(ct);
        }
    }
}