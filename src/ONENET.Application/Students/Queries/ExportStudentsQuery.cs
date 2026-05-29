/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Application (Queries)
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;
using ONENET.Domain.Repositories;

namespace ONENET.Application.Students.Queries
{
    public record ExportStudentsQuery(
        string? SearchQuery,
        string CurrentUser,
        string IpAddress
    ) : IRequest<ExportStudentsResponse>;

    public record ExportStudentsResponse(byte[] FileContents, string ContentType, string FileName);

    public class ExportStudentsQueryHandler : IRequestHandler<ExportStudentsQuery, ExportStudentsResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IExcelService _excelService;
        private readonly IAuditLogRepository _auditLogRepository;

        public ExportStudentsQueryHandler(
            IStudentRepository studentRepository, 
            IExcelService excelService,
            IAuditLogRepository auditLogRepository)
        {
            _studentRepository = studentRepository;
            _excelService = excelService;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<ExportStudentsResponse> Handle(ExportStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllActiveAsync(request.SearchQuery, cancellationToken);
            var fileBytes = await _excelService.ExportStudentsAsync(students);

            // Audit Trail
            var auditLog = new AuditLog
            {
                LogId = $"LOG-{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 15).ToUpper()}",
                Username = request.CurrentUser,
                Action = "EXPORT",
                ObjectAffected = "DANH_SACH_HOC_SINH",
                Timestamp = DateTimeOffset.UtcNow,
                IpAddress = request.IpAddress,
                NewValues = "{\"search_query\": \"" + (request.SearchQuery ?? "") + "\"}"
            };
            await _auditLogRepository.AddAsync(auditLog, cancellationToken);

            var fileName = $"ONENET_HocSinh_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            return new ExportStudentsResponse(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}