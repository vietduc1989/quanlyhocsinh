using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Domain.Interfaces
{
    public interface IAuditService
    {
        Task LogAsync<TEntity>(string actionType, TEntity entity, string userName, object? changes = null, CancellationToken cancellationToken = default) where TEntity : class;
    }
}
