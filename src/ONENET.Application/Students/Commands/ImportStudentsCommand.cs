/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Application (Commands)
 */

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;
using ONENET.Domain.Repositories;

namespace ONENET.Application.Students.Commands
{
    public record ImportStudentsCommand(
        Stream FileStream,
        string CurrentUser,
        string IpAddress
    ) : IRequest<ImportStudentsResponse>;

    public record ImportStudentsResponse(
        bool Success, 
        string Message, 
        int SuccessCount = 0, 
        byte[]? ErrorReportBytes = null
    );

    public class ImportStudentsCommandHandler : IRequestHandler<ImportStudentsCommand, ImportStudentsResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IExcelService _excelService;
        private readonly IAuditLogRepository _auditLogRepository;

        public ImportStudentsCommandHandler(
            IStudentRepository studentRepository,
            IExcelService excelService,
            IAuditLogRepository auditLogRepository)
        {
            _studentRepository = studentRepository;
            _excelService = excelService;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<ImportStudentsResponse> Handle(ImportStudentsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var (validStudents, errorRows) = await _excelService.ParseAndValidateImportFileAsync(request.FileStream);

                // BR-03: Nếu có dòng bị lỗi trong file Excel, Rollback toàn bộ và trả về file báo cáo lỗi chi tiết
                if (errorRows.Count > 0)
                {
                    var errorReportBytes = await _excelService.GenerateErrorReportAsync(errorRows);
                    return new ImportStudentsResponse(
                        false, 
                        "Phát hiện dữ liệu không hợp lệ. Vui lòng tải file đính kèm lỗi bên dưới để sửa đổi.", 
                        0, 
                        errorReportBytes
                    );
                }

                // Tự động sinh mã học sinh duy nhất cho từng bản ghi hợp lệ
                var baseDate = DateOnly.FromDateTime(DateTime.Today);
                int successCount = 0;

                foreach (var student in validStudents)
                {
                    string studentCode = await _studentRepository.GenerateNextStudentCodeAsync(baseDate, cancellationToken);
                    student.StudentCode = studentCode;
                    student.CreatedAt = DateTimeOffset.UtcNow;
                    student.UpdatedAt = DateTimeOffset.UtcNow;

                    await _studentRepository.AddAsync(student, cancellationToken);
                    successCount++;
                }

                // Ghi nhận log
                var auditLog = new AuditLog
                {
                    LogId = $"LOG-{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 15).ToUpper()}",
                    Username = request.CurrentUser,
                    Action = "IMPORT",
                    ObjectAffected = $"IMPORT_{successCount}_HOC_SINH",
                    Timestamp = DateTimeOffset.UtcNow,
                    IpAddress = request.IpAddress,
                    NewValues = JsonSerializer.Serialize(new { total_imported = successCount })
                };
                await _auditLogRepository.AddAsync(auditLog, cancellationToken);

                return new ImportStudentsResponse(true, $"Import thành công {successCount}/{successCount} bản ghi học sinh.", successCount);
            }
            catch (Exception ex)
            {
                return new ImportStudentsResponse(false, $"Có lỗi xảy ra trong quá trình xử lý Import: {ex.Message}");
            }
        }
    }
}