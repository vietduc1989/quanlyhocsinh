// QUAN-20260530-2301
using ONENET.Domain.Entities;

namespace ONENET.Domain.Interfaces;

public interface ITrangThaiHocSinhRepository
{
    Task<TrangThaiHocSinh?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<TrangThaiHocSinh>> GetAllAsync(CancellationToken ct = default);
}