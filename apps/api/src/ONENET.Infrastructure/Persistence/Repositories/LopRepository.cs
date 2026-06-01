// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;

namespace ONENET.Infrastructure.Persistence.Repositories;

public class LopRepository : ILopRepository
{
    private readonly AppDbContext _dbContext;

    public LopRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Lop?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.Lops
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted, ct);
    }

    public async Task<IReadOnlyList<Lop>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbContext.Lops
            .AsNoTracking()
            .Where(l => !l.IsDeleted)
            .OrderBy(l => l.TenLop)
            .ToListAsync(ct);
    }
}