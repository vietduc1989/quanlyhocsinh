/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Application (Interfaces)
 */

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ONENET.Domain.Entities;

namespace ONENET.Application.Common.Interfaces
{
    public interface IExcelService
    {
        Task<byte[]> ExportStudentsAsync(IEnumerable<Student> students);
        Task<(List<Student> ValidStudents, List<ExcelErrorRow> ErrorRows)> ParseAndValidateImportFileAsync(Stream fileStream);
        Task<byte[]> GenerateErrorReportAsync(List<ExcelErrorRow> errorRows);
    }

    public class ExcelErrorRow
    {
        public int RowIndex { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ParentPhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}