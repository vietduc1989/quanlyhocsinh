// QUAN-20260604-153038
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.HocSinhs.DTOs;
using ONENET.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.HocSinhs.Commands
{
    public class UpdateHocSinhCommand : IRequest<Result<HocSinhDto>>
    {
        public Guid Id { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public DateOnly NgaySinh { get; set; }
        public GioiTinh GioiTinh { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoaiPH { get; set; }
        public string? EmailPH { get; set; }
        public Guid LopHocId { get; set; }
        public TrangThaiHocSinh TrangThai { get; set; }
        // MaHocSinh not included as it's not editable (BR01)
    }

    public class UpdateHocSinhCommandHandler : IRequestHandler<UpdateHocSinhCommand, Result<HocSinhDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        // In a real application, ICurrentUserService would provide UpdatedBy
        private readonly string _currentUserId = "AdminUser"; // Placeholder for UpdatedBy

        public UpdateHocSinhCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<HocSinhDto>> Handle(UpdateHocSinhCommand request, CancellationToken cancellationToken)
        {
            var hocSinh = await _context.HocSinhs.FindAsync(new object[] { request.Id }, cancellationToken);

            if (hocSinh == null)
            {
                return Result<HocSinhDto>.Failure("Học sinh không tìm thấy.");
            }

            // BR01: MaHocSinh is not editable after creation, so we don't update it here.
            hocSinh.HoTen = request.HoTen;
            hocSinh.NgaySinh = request.NgaySinh;
            hocSinh.GioiTinh = request.GioiTinh;
            hocSinh.DiaChi = request.DiaChi;
            hocSinh.SoDienThoaiPH = request.SoDienThoaiPH;
            hocSinh.EmailPH = request.EmailPH;
            hocSinh.LopHocId = request.LopHocId;
            hocSinh.TrangThai = request.TrangThai;
            hocSinh.UpdatedAt = DateTimeOffset.UtcNow;
            hocSinh.UpdatedBy = _currentUserId;

            await _context.SaveChangesAsync(cancellationToken);

            return Result<HocSinhDto>.Success(_mapper.Map<HocSinhDto>(hocSinh));
        }
    }
}