// QUAN-20260604-153038
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.HocSinhs.DTOs;
using ONENET.Domain.Entities;
using ONENET.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.HocSinhs.Commands
{
    public class CreateHocSinhCommand : IRequest<Result<HocSinhDto>>
    {
        // MaHocSinh is auto-generated, not provided by user
        public string HoTen { get; set; } = string.Empty;
        public DateOnly NgaySinh { get; set; }
        public GioiTinh GioiTinh { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoaiPH { get; set; }
        public string? EmailPH { get; set; }
        public Guid LopHocId { get; set; }
        public TrangThaiHocSinh TrangThai { get; set; }
    }

    public class CreateHocSinhCommandHandler : IRequestHandler<CreateHocSinhCommand, Result<HocSinhDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        // In a real application, ICurrentUserService would provide CreatedBy
        private readonly string _currentUserId = "AdminUser"; // Placeholder for CreatedBy

        public CreateHocSinhCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<HocSinhDto>> Handle(CreateHocSinhCommand request, CancellationToken cancellationToken)
        {
            var hocSinh = new HocSinh
            {
                Id = Guid.NewGuid(),
                MaHocSinh = $"HS-{Guid.NewGuid().ToString()[..8].ToUpper()}", // BR01: Auto-generated unique code
                HoTen = request.HoTen,
                NgaySinh = request.NgaySinh,
                GioiTinh = request.GioiTinh,
                DiaChi = request.DiaChi,
                SoDienThoaiPH = request.SoDienThoaiPH,
                EmailPH = request.EmailPH,
                LopHocId = request.LopHocId,
                TrangThai = request.TrangThai,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = _currentUserId,
                IsDeleted = false
            };

            _context.HocSinhs.Add(hocSinh);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<HocSinhDto>.Success(_mapper.Map<HocSinhDto>(hocSinh));
        }
    }
}