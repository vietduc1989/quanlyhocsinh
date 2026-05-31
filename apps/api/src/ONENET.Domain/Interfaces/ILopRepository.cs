// QUAN-20260530-2301
using ONENET.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Domain.Interfaces
{
    public interface ILopRepository
    {
        Task<Lop?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Lop?> GetByTenLopAsync(string tenLop, CancellationToken ct = default);
        Task AddAsync(Lop lop, CancellationToken ct = default); // For seeding or future management
    }
}