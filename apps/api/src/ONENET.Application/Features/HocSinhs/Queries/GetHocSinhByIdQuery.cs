// QUAN-20260604-153038
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.HocSinhs.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.HocSinhs.Queries
{
    public class GetHocSinhByIdQuery : IRequest<Result<HocSinhDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetHocSinhByIdQueryHandler : IRequestHandler<GetHocSinhByIdQuery, Result<HocSinhDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetHocSinhByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<HocSinhDto>> Handle(GetHocSinhByIdQuery request, CancellationToken cancellationToken)
        {
            var hocSinh = await _context.HocSinhs
                .AsNoTracking()
                .Include(h => h.LopHoc)
                .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

            if (hocSinh == null)
            {
                return Result<HocSinhDto>.Failure("Học sinh không tìm thấy.");
            }

            return Result<HocSinhDto>.Success(_mapper.Map<HocSinhDto>(hocSinh));
        }
    }
}