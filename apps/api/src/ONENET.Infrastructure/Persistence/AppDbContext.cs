// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Common;
using ONENET.Domain.Entities;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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

        // Add new DbSets for Student management
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Lop> Lops => Set<Lop>();
        public DbSet<TrangThaiHocSinh> TrangThaiHocSinhs => Set<TrangThaiHocSinh>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Apply configurations for entities in this assembly
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
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
                }
            }

            return await base.SaveChangesAsync(ct);
        }
    }
}