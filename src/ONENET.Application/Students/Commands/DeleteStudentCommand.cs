/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Application (Commands)
 */

using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ONENET.Domain.Entities;
using ONENET.Domain.Repositories;

namespace ONENET.Application.Students.Commands
{
    public record DeleteStudentCommand(
        Guid Id,
        string CurrentUser,
        string IpAddress
    ) : IRequest<DeleteStudentResponse>;

    public record DeleteStudentResponse(bool Success, string Message);

    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, DeleteStudentResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAuditLogRepository _auditLogRepository;

        public DeleteStudentCommandHandler(
            IStudentRepository studentRepository, 
            IAuditLogRepository auditLogRepository)
        {
            _studentRepository = studentRepository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<DeleteStudentResponse> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (student == null || student.IsDeleted)
            {
                return new DeleteStudentResponse(false, "Không tìm thấy thông tin học sinh cần xóa.");
            }

            // BR-03: Soft Delete
            student.IsDeleted = true;
            student.UpdatedAt = DateTimeOffset.UtcNow;

            _studentRepository.Update(student);

            // Audit Trail
            var auditLog = new AuditLog
            {
                LogId = $"LOG-{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 15).ToUpper()}",
                Username = request.CurrentUser,
                Action = "DELETE",
                ObjectAffected = student.StudentCode,
                Timestamp = DateTimeOffset.UtcNow,
                IpAddress = request.IpAddress,
                OldValues = JsonSerializer.Serialize(new { is_deleted = false, student.FullName, student.StudentCode }),
                NewValues = JsonSerializer.Serialize(new { is_deleted = true })
            };
            await _auditLogRepository.AddAsync(auditLog, cancellationToken);

            return new DeleteStudentResponse(true, "Xóa thông tin học sinh thành công (Soft Delete).");
        }
    }
}