// QUAN-20260530-2301
using ONENET.Domain.Entities;

namespace ONENET.Domain.Interfaces;

public interface ILopRepository
{
    Task<Lop?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Lop>> GetAllAsync(CancellationToken ct = default);
}