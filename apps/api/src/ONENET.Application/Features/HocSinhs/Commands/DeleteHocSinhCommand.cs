// QUAN-20260604-153038
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Common.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.HocSinhs.Commands
{
    public class DeleteHocSinhCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class DeleteHocSinhCommandHandler : IRequestHandler<DeleteHocSinhCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        // In a real application, ICurrentUserService would provide UpdatedBy
        private readonly string _currentUserId = "AdminUser"; // Placeholder for UpdatedBy

        public DeleteHocSinhCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteHocSinhCommand request, CancellationToken cancellationToken)
        {
            var hocSinh = await _context.HocSinhs.FindAsync(new object[] { request.Id }, cancellationToken);

            if (hocSinh == null)
            {
                return Result.Failure("Học sinh không tìm thấy.");
            }

            // FR06: Ngăn chặn xóa học sinh nếu có dữ liệu liên quan (active data)
            // This is a soft delete, so we check for active related data.
            // If the related data also supports soft delete, you might filter by IsDeleted = false.
            var hasActiveRelatedData = await _context.HocSinhDiems
                .AnyAsync(d => d.HocSinhId == hocSinh.Id && !d.IsDeleted, cancellationToken); // Assuming HocSinhDiem also uses soft delete

            if (hasActiveRelatedData)
            {
                return Result.Failure("Không thể xóa học sinh này vì có dữ liệu liên quan. Vui lòng xóa các dữ liệu liên quan trước hoặc chuyển trạng thái học sinh thành 'Đã chuyển trường'/'Tạm dừng' thay vì xóa.");
            }

            hocSinh.IsDeleted = true; // Soft delete
            hocSinh.UpdatedAt = DateTimeOffset.UtcNow;
            hocSinh.UpdatedBy = _currentUserId;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}