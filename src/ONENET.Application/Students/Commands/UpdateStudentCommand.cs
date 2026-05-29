/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Application (Commands)
 */

using System;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ONENET.Domain.Entities;
using ONENET.Domain.Repositories;

namespace ONENET.Application.Students.Commands
{
    public record UpdateStudentCommand(
        Guid Id,
        string FullName,
        DateOnly DateOfBirth,
        string Gender,
        string? Address,
        string ParentPhone,
        string? Email,
        string CurrentUser,
        string IpAddress
    ) : IRequest<UpdateStudentResponse>;

    public record UpdateStudentResponse(bool Success, string Message, Student? Data);

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, UpdateStudentResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAuditLogRepository _auditLogRepository;

        public UpdateStudentCommandHandler(
            IStudentRepository studentRepository, 
            IAuditLogRepository auditLogRepository)
        {
            _studentRepository = studentRepository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<UpdateStudentResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (student == null || student.IsDeleted)
            {
                return new UpdateStudentResponse(false, "Không tìm thấy thông tin học sinh yêu cầu chỉnh sửa.", null);
            }

            // BR-02: Validation Rules
            if (string.IsNullOrWhiteSpace(request.FullName))
                return new UpdateStudentResponse(false, "Họ tên là trường bắt buộc nhập, không được bỏ trống.", null);

            if (!Regex.IsMatch(request.FullName, @"^[a-zA-Z\s\p{L}]+$"))
                return new UpdateStudentResponse(false, "Họ tên không được phép chứa các ký tự số hoặc ký tự đặc biệt.", null);

            var currentDate = DateOnly.FromDateTime(DateTime.Today);
            if (request.DateOfBirth >= currentDate)
                return new UpdateStudentResponse(false, "Ngày sinh của học sinh phải luôn nhỏ hơn ngày hiện tại.", null);

            if (string.IsNullOrWhiteSpace(request.ParentPhone))
                return new UpdateStudentResponse(false, "SĐT phụ huynh là trường bắt buộc nhập.", null);

            if (!Regex.IsMatch(request.ParentPhone, @"^(03|05|07|08|09)\d{8}$"))
                return new UpdateStudentResponse(false, "SĐT phụ huynh phải gồm 10 chữ số và bắt đầu bằng các đầu số hợp lệ (03, 05, 07, 08, 09).", null);

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(request.Email, emailPattern))
                    return new UpdateStudentResponse(false, "Email không hợp lệ.", null);
            }

            if (request.Gender != "Nam" && request.Gender != "Nữ" && request.Gender != "Khác")
                return new UpdateStudentResponse(false, "Giới tính không hợp lệ (Chỉ chấp nhận Nam, Nữ, Khác).", null);

            // Snapshot old values for Audit Trail
            var oldValuesJson = JsonSerializer.Serialize(new
            {
                student.FullName,
                student.DateOfBirth,
                student.Gender,
                student.Address,
                student.ParentPhone,
                student.Email
            });

            // Update fields
            student.FullName = request.FullName.Trim();
            student.DateOfBirth = request.DateOfBirth;
            student.Gender = request.Gender;
            student.Address = request.Address?.Trim();
            student.ParentPhone = request.ParentPhone.Trim();
            student.Email = request.Email?.Trim();
            student.UpdatedAt = DateTimeOffset.UtcNow;

            _studentRepository.Update(student);

            // Audit Trail
            var auditLog = new AuditLog
            {
                LogId = $"LOG-{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 15).ToUpper()}",
                Username = request.CurrentUser,
                Action = "UPDATE",
                ObjectAffected = student.StudentCode,
                Timestamp = DateTimeOffset.UtcNow,
                IpAddress = request.IpAddress,
                OldValues = oldValuesJson,
                NewValues = JsonSerializer.Serialize(new
                {
                    student.FullName,
                    student.DateOfBirth,
                    student.Gender,
                    student.Address,
                    student.ParentPhone,
                    student.Email
                })
            };
            await _auditLogRepository.AddAsync(auditLog, cancellationToken);

            return new UpdateStudentResponse(true, "Cập nhật thông tin học sinh thành công.", student);
        }
    }
}