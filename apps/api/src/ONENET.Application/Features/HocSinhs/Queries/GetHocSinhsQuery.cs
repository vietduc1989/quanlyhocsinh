// QUAN-20260604-153038
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.HocSinhs.DTOs;
using ONENET.Domain.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.HocSinhs.Queries
{
    public class GetHocSinhsQuery : IRequest<Result<PaginatedList<HocSinhDto>>>
    {
        public string? SearchTerm { get; set; } // FR07: Mã Học sinh / Họ và Tên
        public Guid? LopHocId { get; set; } // FR07: Lọc theo Lớp Học
        public TrangThaiHocSinh? TrangThai { get; set; } // FR07: Lọc theo Trạng Thái
        public int PageNumber { get; set; } = 1; // FR08: Phân trang
        public int PageSize { get; set; } = 10; // FR08: Phân trang
    }

    public class GetHocSinhsQueryHandler : IRequestHandler<GetHocSinhsQuery, Result<PaginatedList<HocSinhDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetHocSinhsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<HocSinhDto>>> Handle(GetHocSinhsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.HocSinhs
                .AsNoTracking() // NFR: Performance
                .Include(h => h.LopHoc)
                .OrderBy(h => h.MaHocSinh) // FR01: Default sort
                .AsQueryable();

            // FR07: Search by MaHocSinh or HoTen
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(h => h.MaHocSinh.ToLower().Contains(request.SearchTerm.ToLower()) ||
                                         h.HoTen.ToLower().Contains(request.SearchTerm.ToLower()));
            }

            // FR07: Filter by LopHoc
            if (request.LopHocId.HasValue && request.LopHocId != Guid.Empty)
            {
                query = query.Where(h => h.LopHocId == request.LopHocId.Value);
            }

            // FR07: Filter by TrangThai
            if (request.TrangThai.HasValue)
            {
                query = query.Where(h => h.TrangThai == request.TrangThai.Value);
            }

            var paginatedList = await PaginatedList<HocSinhDto>.CreateAsync(
                query.ProjectTo<HocSinhDto>(_mapper.ConfigurationProvider),
                request.PageNumber,
                request.PageSize);

            return Result<PaginatedList<HocSinhDto>>.Success(paginatedList);
        }
    }
}