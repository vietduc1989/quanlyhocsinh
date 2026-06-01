// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;

namespace ONENET.Infrastructure.Persistence.Repositories;

public class TrangThaiHocSinhRepository : ITrangThaiHocSinhRepository
{
    private readonly AppDbContext _dbContext;

    public TrangThaiHocSinhRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TrangThaiHocSinh?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.TrangThaiHocSinhs
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted, ct);
    }

    public async Task<IReadOnlyList<TrangThaiHocSinh>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbContext.TrangThaiHocSinhs
            .AsNoTracking()
            .Where(t => !t.IsDeleted)
            .OrderBy(t => t.TenTrangThai)
            .ToListAsync(ct);
    }
}