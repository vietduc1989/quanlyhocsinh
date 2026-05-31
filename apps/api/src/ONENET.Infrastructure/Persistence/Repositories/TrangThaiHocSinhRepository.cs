// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class TrangThaiHocSinhRepository : ITrangThaiHocSinhRepository
    {
        private readonly AppDbContext _context;

        public TrangThaiHocSinhRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TrangThaiHocSinh?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.TrangThaiHocSinhs.FindAsync(new object[] { id }, ct);
        }

        public async Task<TrangThaiHocSinh?> GetByMaTrangThaiAsync(string maTrangThai, CancellationToken ct = default)
        {
            return await _context.TrangThaiHocSinhs.AsNoTracking().FirstOrDefaultAsync(t => t.MaTrangThai == maTrangThai, ct);
        }

        public async Task AddAsync(TrangThaiHocSinh trangThai, CancellationToken ct = default)
        {
            await _context.TrangThaiHocSinhs.AddAsync(trangThai, ct);
        }
    }
}