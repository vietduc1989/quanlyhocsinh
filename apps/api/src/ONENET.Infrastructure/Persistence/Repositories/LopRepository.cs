using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class LopRepository : Repository<Lop>, ILopRepository
    {
        public LopRepository(AppDbContext context) : base(context)
        {
        }
    }
}