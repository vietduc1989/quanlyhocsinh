// QUAN-20260530-2301
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.Students.Dtos;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using ONENET.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.Students.Queries
{
    public class GetStudentsPagedQueryHandler : IRequestHandler<GetStudentsPagedQuery, PagedList<StudentSummaryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStudentsPagedQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<StudentSummaryDto>> Handle(GetStudentsPagedQuery request, CancellationToken ct)
        {
            IQueryable<Student> query = _context.Students
                                          .AsNoTracking()
                                          .Include(s => s.Lop)
                                          .Include(s => s.TrangThai);

            // QUAN-20260530-2301-FR07: Search
            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                var searchLower = request.SearchQuery.ToLower();
                query = query.Where(s =>
                    s.MaHocSinh.ToLower().Contains(searchLower) ||
                    s.HoVaTen.ToLower().Contains(searchLower)
                );
            }

            // QUAN-20260530-2301-FR07: Filter by LopId
            if (request.LopId.HasValue)
            {
                query = query.Where(s => s.LopId == request.LopId.Value);
            }

            // QUAN-20260530-2301-FR07: Filter by TrangThaiId
            if (request.TrangThaiId.HasValue)
            {
                query = query.Where(s => s.TrangThaiId == request.TrangThaiId.Value);
            }

            // Sorting
            query = request.SortBy?.ToLower() switch
            {
                "mahocsinh" => request.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(s => s.MaHocSinh) : query.OrderBy(s => s.MaHocSinh),
                "hovaten" => request.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(s => s.HoVaTen) : query.OrderBy(s => s.HoVaTen),
                "lophoc" => request.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(s => s.Lop.TenLop) : query.OrderBy(s => s.Lop.TenLop),
                "trangthai" => request.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(s => s.TrangThai.TenTrangThai) : query.OrderBy(s => s.TrangThai.TenTrangThai),
                _ => query.OrderBy(s => s.HoVaTen) // Default sort
            };

            var projectedQuery = query.ProjectTo<StudentSummaryDto>(_mapper.ConfigurationProvider);

            // QUAN-20260530-2301-FR08: Pagination
            return await PagedList<StudentSummaryDto>.CreateAsync(
                projectedQuery,
                request.PageNumber,
                request.PageSize
            );
        }
    }
}
