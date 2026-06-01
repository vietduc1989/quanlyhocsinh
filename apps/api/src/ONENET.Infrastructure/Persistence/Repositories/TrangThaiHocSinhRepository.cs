<!-- QUAN-20260530-2301 -->
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class TrangThaiHocSinhRepository : Repository<TrangThaiHocSinh>, ITrangThaiHocSinhRepository
    {
        public TrangThaiHocSinhRepository(AppDbContext context) : base(context)
        {
        }
    }
}