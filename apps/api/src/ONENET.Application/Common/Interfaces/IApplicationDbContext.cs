using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Student> Students { get; }
        DbSet<Class> Classes { get; }
        DbSet<Subject> Subjects { get; }
        DbSet<Score> Scores { get; }
        DbSet<Teacher> Teachers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
