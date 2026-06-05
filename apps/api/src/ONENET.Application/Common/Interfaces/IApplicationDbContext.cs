// QUAN-20260604-153038
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<HocSinh> HocSinhs { get; }
        DbSet<LopHoc> LopHocs { get; }
        DbSet<HocSinhDiem> HocSinhDiems { get; } // For FR06 related data check

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}