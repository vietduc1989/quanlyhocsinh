// QUAN-20260604-153038
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.LopHocs.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.LopHocs.Queries
{
    public class GetLopHocsForFilterQuery : IRequest<Result<IReadOnlyList<LopHocLookupDto>>>
    {
        public string? SearchTerm { get; set; }
    }

    public class GetLopHocsForFilterQueryHandler : IRequestHandler<GetLopHocsForFilterQuery, Result<IReadOnlyList<LopHocLookupDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLopHocsForFilterQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<LopHocLookupDto>>> Handle(GetLopHocsForFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _context.LopHocs
                .AsNoTracking()
                .OrderBy(l => l.TenLop)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(l => l.TenLop.ToLower().Contains(request.SearchTerm.ToLower()));
            }

            var lopHocs = await query
                .ProjectTo<LopHocLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IReadOnlyList<LopHocLookupDto>>.Success(lopHocs);
        }
    }
}