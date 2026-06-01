using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Common.Interfaces
{
    public interface IScorePermissionService
    {
        Task<bool> HasPermissionToUpdateScoreAsync(string maGiaoVien, System.Guid lopId, System.Guid monHocId, CancellationToken cancellationToken);
    }
}
