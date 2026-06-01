// QUAN-20260530-2302
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Classes.Commands
{
    public class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IClassRepository _classRepository; // Use repository for business logic checks
        private readonly ILogger<DeleteClassCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClassCommandHandler(IApplicationDbContext context, IClassRepository classRepository,
            ILogger<DeleteClassCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _context = context;
            _classRepository = classRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteClassCommand request, CancellationToken ct)
        {
            var classToDelete = await _context.Classes
                .Include(c => c.HomeroomTeacher) // Include to get teacher name for conflict message
                .SingleOrDefaultAsync(c => c.Id == request.Id, ct);

            if (classToDelete == null)
            {
                throw new NotFoundException($"Lớp học với ID '{request.Id}' không tìm thấy.");
            }

            // Business rule: Prevent deletion if class has students
            var studentCount = await _classRepository.GetStudentCountInClassAsync(request.Id, ct);
            if (studentCount > 0)
            {
                throw new BusinessRuleException(
                    $"Không thể xóa lớp học '{classToDelete.ClassCode}' vì hiện có {studentCount} học sinh đang theo học. " +
                    "Vui lòng chuyển hoặc xóa học sinh trước khi thực hiện.");
            }

            // Business rule: Prevent deletion if class has a homeroom teacher assigned
            if (classToDelete.HomeroomTeacherId.HasValue)
            {
                throw new BusinessRuleException(
                    $"Không thể xóa lớp học '{classToDelete.ClassCode}' vì đang được phân công cho Giáo viên " +
                    $"{(classToDelete.HomeroomTeacher != null ? classToDelete.HomeroomTeacher.FullName : "không xác định")}. " +
                    "Vui lòng thay đổi Giáo viên chủ nhiệm trước khi xóa.");
            }
            
            _classRepository.Delete(classToDelete); // Mark for deletion (soft delete handled by repository implicitly)
            await _unitOfWork.SaveChangesAsync(ct);

            _logger.LogInformation("Class deleted: {ClassId}", request.Id);
        }
    }
}