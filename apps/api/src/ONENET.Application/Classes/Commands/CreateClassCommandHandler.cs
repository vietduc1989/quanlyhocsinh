// QUAN-20260530-2302
using MediatR;
using Microsoft.Extensions.Logging;
using ONENET.Application.Classes.DTOs;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Classes.Commands
{
    public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, ClassIdDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<CreateClassCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateClassCommandHandler(IApplicationDbContext context, ICurrentUser currentUser,
            ILogger<CreateClassCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ClassIdDto> Handle(CreateClassCommand request, CancellationToken ct)
        {
            var currentUserId = _currentUser.UserId;
            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new UnauthorizedAccessException("Người dùng hiện tại không hợp lệ.");
            }

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

            var classEntity = Class.Create(
                request.ClassCode,
                request.ClassName,
                request.SchoolYear,
                request.HomeroomTeacherId,
                currentUserId
            );

            _context.Classes.Add(classEntity);
            await _unitOfWork.SaveChangesAsync(ct);

            _logger.LogInformation("Class created: {ClassId} by {UserId}", classEntity.Id, currentUserId);

            return new ClassIdDto { Id = classEntity.Id };
        }
    }
}