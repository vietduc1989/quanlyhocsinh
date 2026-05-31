// QUAN-20260530-2301
using ONENET.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Domain.Interfaces
{
    public interface ITrangThaiHocSinhRepository
    {
        Task<TrangThaiHocSinh?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<TrangThaiHocSinh?> GetByMaTrangThaiAsync(string maTrangThai, CancellationToken ct = default);
        Task AddAsync(TrangThaiHocSinh trangThai, CancellationToken ct = default); // For seeding or future management
    }
}