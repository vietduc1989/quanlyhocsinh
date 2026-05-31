// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class LopRepository : ILopRepository
    {
        private readonly AppDbContext _context;

        public LopRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Lop?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Lops.FindAsync(new object[] { id }, ct);
        }

        public async Task<Lop?> GetByTenLopAsync(string tenLop, CancellationToken ct = default)
        {
            return await _context.Lops.AsNoTracking().FirstOrDefaultAsync(l => l.TenLop == tenLop, ct);
        }

        public async Task AddAsync(Lop lop, CancellationToken ct = default)
        {
            await _context.Lops.AddAsync(lop, ct);
        }
    }
}