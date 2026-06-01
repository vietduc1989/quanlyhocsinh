using System.Threading;
using System.Threading.Tasks;
using ONENET.Domain.Entities;

namespace ONENET.Domain.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<bool> IsMaHocSinhUniqueAsync(string maHocSinh, Guid? studentId = null, CancellationToken ct = default);
        Task<bool> HasRelatedDataAsync(Guid studentId, CancellationToken ct = default);
    }
}