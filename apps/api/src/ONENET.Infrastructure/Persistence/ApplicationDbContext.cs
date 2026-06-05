// QUAN-20260604-153038
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;
using ONENET.Infrastructure.Persistence.Configurations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<HocSinh> HocSinhs => Set<HocSinh>();
        public DbSet<LopHoc> LopHocs => Set<LopHoc>();
        public DbSet<HocSinhDiem> HocSinhDiems => Set<HocSinhDiem>(); // For FR06

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Apply configurations from current assembly
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Global Query Filter for Soft Delete (Guideline 5. Database)
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType).HasQueryFilter(e => !((BaseEntity)e).IsDeleted);
                }
            }

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // Can add interceptors for CreatedAt, CreatedBy, UpdatedAt, UpdatedBy here
            // For simplicity, it's currently handled in Commands.
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}