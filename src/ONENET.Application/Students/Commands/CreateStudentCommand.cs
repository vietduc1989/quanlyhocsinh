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
    public record CreateStudentCommand(
        string FullName,
        DateOnly DateOfBirth,
        string Gender,
        string? Address,
        string ParentPhone,
        string? Email,
        string CurrentUser,
        string IpAddress
    ) : IRequest<CreateStudentResponse>;

    public record CreateStudentResponse(bool Success, string Message, Student? Data);

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CreateStudentResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAuditLogRepository _auditLogRepository;

        public CreateStudentCommandHandler(
            IStudentRepository studentRepository, 
            IAuditLogRepository auditLogRepository)
        {
            _studentRepository = studentRepository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<CreateStudentResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            // BR-02: Validation Rules
            if (string.IsNullOrWhiteSpace(request.FullName))
                return new CreateStudentResponse(false, "Họ tên là trường bắt buộc nhập, không được bỏ trống.", null);

            // Kiểm tra họ tên không chứa số hoặc ký tự đặc biệt
            if (!Regex.IsMatch(request.FullName, @"^[a-zA-Z\s\p{L}]+$"))
                return new CreateStudentResponse(false, "Họ tên không được phép chứa các ký tự số hoặc ký tự đặc biệt.", null);

            // Kiểm tra ngày sinh nhỏ hơn hiện tại
            var currentDate = DateOnly.FromDateTime(DateTime.Today);
            if (request.DateOfBirth >= currentDate)
                return new CreateStudentResponse(false, "Ngày sinh của học sinh phải luôn nhỏ hơn ngày hiện tại.", null);

            // Kiểm tra SĐT phụ huynh
            if (string.IsNullOrWhiteSpace(request.ParentPhone))
                return new CreateStudentResponse(false, "SĐT phụ huynh là trường bắt buộc nhập.", null);

            if (!Regex.IsMatch(request.ParentPhone, @"^(03|05|07|08|09)\d{8}$"))
                return new CreateStudentResponse(false, "SĐT phụ huynh phải gồm 10 chữ số và bắt đầu bằng các đầu số hợp lệ (03, 05, 07, 08, 09).", null);

            // Kiểm tra Email (nếu nhập)
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(request.Email, emailPattern))
                    return new CreateStudentResponse(false, "Email không hợp lệ.", null);
            }

            // Kiểm tra giới tính
            if (request.Gender != "Nam" && request.Gender != "Nữ" && request.Gender != "Khác")
                return new CreateStudentResponse(false, "Giới tính không hợp lệ (Chỉ chấp nhận Nam, Nữ, Khác).", null);

            // BR-01: Sinh mã học sinh tự động
            string studentCode = await _studentRepository.GenerateNextStudentCodeAsync(currentDate, cancellationToken);

            var student = new Student
            {
                StudentCode = studentCode,
                FullName = request.FullName.Trim(),
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Address = request.Address?.Trim(),
                ParentPhone = request.ParentPhone.Trim(),
                Email = request.Email?.Trim(),
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            await _studentRepository.AddAsync(student, cancellationToken);

            // Audit Trail
            var auditLog = new AuditLog
            {
                LogId = $"LOG-{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 15).ToUpper()}",
                Username = request.CurrentUser,
                Action = "CREATE",
                ObjectAffected = student.StudentCode,
                Timestamp = DateTimeOffset.UtcNow,
                IpAddress = request.IpAddress,
                NewValues = JsonSerializer.Serialize(new
                {
                    student.StudentCode,
                    student.FullName,
                    student.DateOfBirth,
                    student.Gender,
                    student.Address,
                    student.ParentPhone,
                    student.Email
                })
            };
            await _auditLogRepository.AddAsync(auditLog, cancellationToken);

            return new CreateStudentResponse(true, "Thêm mới học sinh thành công.", student);
        }
    }
}