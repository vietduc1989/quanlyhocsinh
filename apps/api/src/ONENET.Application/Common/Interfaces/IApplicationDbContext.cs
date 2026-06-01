using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Student> Students { get; }
        DbSet<AuditLog> AuditLogs { get; }
        DbSet<Lop> Lops { get; }
        DbSet<TrangThaiHocSinh> TrangThaiHocSinhs { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
