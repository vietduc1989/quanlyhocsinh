<!-- QUAN-20260530-2301 -->
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.Students.DTOs;
using ONENET.Application.Features.Students.Queries;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;

namespace ONENET.Application.Features.Students.Handlers
{
    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, PagedListDto<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetStudentsQueryHandler> _logger;

        public GetStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper, ILogger<GetStudentsQueryHandler> logger)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedListDto<StudentDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var studentsQuery = ((ONENET.Infrastructure.Persistence.Repositories.StudentRepository)_studentRepository)
                .GetQueryableStudentsWithLopAndTrangThai();

            // Apply search filter (Mã/Tên)
            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                string searchLower = request.SearchQuery.ToLower();
                studentsQuery = studentsQuery.Where(s =>
                    s.MaHocSinh.ToLower().Contains(searchLower) ||
                    s.HoVaTen.ToLower().Contains(searchLower)
                );
            }

            // Apply filter by LopId
            if (request.LopId.HasValue && request.LopId != Guid.Empty)
            {
                studentsQuery = studentsQuery.Where(s => s.LopId == request.LopId.Value);
            }

            // Apply filter by TrangThaiId
            if (request.TrangThaiId.HasValue && request.TrangThaiId != Guid.Empty)
            {
                studentsQuery = studentsQuery.Where(s => s.TrangThaiId == request.TrangThaiId.Value);
            }

            // Apply sorting
            studentsQuery = request.SortBy?.ToLower() switch
            {
                "mahocsinh" => request.SortOrder?.ToLower() == "desc" ? studentsQuery.OrderByDescending(s => s.MaHocSinh) : studentsQuery.OrderBy(s => s.MaHocSinh),
                "hovaten" => request.SortOrder?.ToLower() == "desc" ? studentsQuery.OrderByDescending(s => s.HoVaTen) : studentsQuery.OrderBy(s => s.HoVaTen),
                "lophoc" => request.SortOrder?.ToLower() == "desc" ? studentsQuery.OrderByDescending(s => s.Lop.TenLop) : studentsQuery.OrderBy(s => s.Lop.TenLop),
                "trangthai" => request.SortOrder?.ToLower() == "desc" ? studentsQuery.OrderByDescending(s => s.TrangThai.TenTrangThai) : studentsQuery.OrderBy(s => s.TrangThai.TenTrangThai),
                _ => studentsQuery.OrderBy(s => s.HoVaTen) // Default sort
            };

            var totalCount = await studentsQuery.CountAsync(cancellationToken);

            var items = await studentsQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .AsNoTracking() // Performance optimization for read-only query
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} students (Page {PageNumber}/{PageSize})", items.Count, request.PageNumber, request.PageSize);

            return new PagedListDto<StudentDto>(items, request.PageNumber, request.PageSize, totalCount);
        }
    }
}