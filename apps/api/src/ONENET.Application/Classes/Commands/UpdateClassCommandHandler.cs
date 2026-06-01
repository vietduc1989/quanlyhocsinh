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
    public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<UpdateClassCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClassCommandHandler(IApplicationDbContext context, ICurrentUser currentUser,
            ILogger<UpdateClassCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateClassCommand request, CancellationToken ct)
        {
            var currentUserId = _currentUser.UserId;
            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new UnauthorizedAccessException("Người dùng hiện tại không hợp lệ.");
            }

            var classToUpdate = await _context.Classes
                .SingleOrDefaultAsync(c => c.Id == request.Id, ct);

            if (classToUpdate == null)
            {
                throw new NotFoundException($"Lớp học với ID '{request.Id}' không tìm thấy.");
            }

            // Optimistic concurrency check
            _context.Entry(classToUpdate).Property(p => p.Version).OriginalValue = request.Version;

            // Check if homeroom teacher exists if ID is provided
            if (request.HomeroomTeacherId.HasValue)
            {
                var teacherExists = await _context.Teachers
                    .AnyAsync(t => t.Id == request.HomeroomTeacherId.Value, ct);
                if (!teacherExists)
                {
                    throw new BusinessRuleException("Giáo viên chủ nhiệm không hợp lệ.");
                }
            }

            classToUpdate.Update(
                request.ClassName,
                request.SchoolYear,
                request.HomeroomTeacherId,
                currentUserId
            );

            _context.Classes.Update(classToUpdate);

            try
            {
                await _unitOfWork.SaveChangesAsync(ct);
                _logger.LogInformation("Class updated: {ClassId} by {UserId}", classToUpdate.Id, currentUserId);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency conflict when updating class {ClassId}", request.Id);
                throw new BusinessRuleException("Lớp học đã được người dùng khác cập nhật. Vui lòng thử lại.");
            }
        }
    }
}