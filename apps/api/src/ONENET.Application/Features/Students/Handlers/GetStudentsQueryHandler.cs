// QUAN-20260530-2301
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.Students.DTOs;
using ONENET.Infrastructure.Persistence; // Direct access to DbContext for query optimization (AsNoTracking)

namespace ONENET.Application.Features.Students.Handlers;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, ApiResponse<PaginatedList<StudentListDto>>>
{
    private readonly AppDbContext _dbContext;

    public GetStudentsQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<PaginatedList<StudentListDto>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Students
            .AsNoTracking() // Performance optimization
            .Include(s => s.Lop)
            .Include(s => s.TrangThaiHocSinh)
            .Where(s => !s.IsDeleted) // Apply soft delete filter explicitly just in case global filter is bypassed or needs to be clearer
            .AsQueryable();

        // QUAN-20260530-2301-SRS01, SRS07: Search by MaHocSinh or HoVaTen
        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            query = query.Where(s =>
                s.MaHocSinh.Contains(request.SearchQuery) ||
                s.HoVaTen.Contains(request.SearchQuery));
        }

        // QUAN-20260530-2301-SRS01, SRS08: Filter by LopId
        if (request.LopId.HasValue && request.LopId != Guid.Empty)
        {
            query = query.Where(s => s.LopId == request.LopId.Value);
        }

        // QUAN-20260530-2301-SRS01, SRS08: Filter by TrangThaiId
        if (request.TrangThaiId.HasValue && request.TrangThaiId != Guid.Empty)
        {
            query = query.Where(s => s.TrangThaiId == request.TrangThaiId.Value);
        }

        // Apply sorting
        query = request.SortBy?.ToLower() switch
        {
            "mahocsinh" => request.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(s => s.MaHocSinh) : query.OrderBy(s => s.MaHocSinh),
            "hovaten" => request.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(s => s.HoVaTen) : query.OrderBy(s => s.HoVaTen),
            "lophoc" => request.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(s => s.Lop.TenLop) : query.OrderBy(s => s.Lop.TenLop),
            "trangthai" => request.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(s => s.TrangThaiHocSinh.TenTrangThai) : query.OrderBy(s => s.TrangThaiHocSinh.TenTrangThai),
            _ => query.OrderBy(s => s.HoVaTen) // Default sort
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(s => new StudentListDto
            {
                Id = s.Id,
                MaHocSinh = s.MaHocSinh,
                HoVaTen = s.HoVaTen,
                TenLop = s.Lop.TenLop,
                TenTrangThai = s.TrangThaiHocSinh.TenTrangThai
            })
            .ToListAsync(cancellationToken);

        var paginatedList = new PaginatedList<StudentListDto>(items, totalCount, request.PageNumber, request.PageSize);

        return ApiResponse<PaginatedList<StudentListDto>>.SuccessResult(paginatedList, "Danh sách học sinh");
    }
}