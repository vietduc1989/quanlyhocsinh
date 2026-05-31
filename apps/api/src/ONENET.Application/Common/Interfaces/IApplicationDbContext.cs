using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Common.Interfaces;

/// <summary>
/// Interface trừu tượng hóa DbContext giúp tách biệt Application Layer khỏi Infrastructure.
/// </summary>
public interface IApplicationDbContext
{
    DbSet<Student> Students { get; }
    DbSet<Class> Classes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}