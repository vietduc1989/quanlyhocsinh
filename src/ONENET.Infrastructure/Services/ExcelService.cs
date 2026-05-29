/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Infrastructure (Excel Services)
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OfficeOpenXml; // Thư viện EPPlus
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        static ExcelService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<byte[]> ExportStudentsAsync(IEnumerable<Student> students)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Danh sách học sinh");

            // Headers
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã học sinh";
            worksheet.Cells[1, 3].Value = "Họ và tên";
            worksheet.Cells[1, 4].Value = "Ngày sinh";
            worksheet.Cells[1, 5].Value = "Giới tính";
            worksheet.Cells[1, 6].Value = "Địa chỉ";
            worksheet.Cells[1, 7].Value = "SĐT Phụ huynh";
            worksheet.Cells[1, 8].Value = "Email";

            using (var range = worksheet.Cells[1, 1, 1, 8])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            int row = 2;
            int idx = 1;
            foreach (var student in students)
            {
                worksheet.Cells[row, 1].Value = idx++;
                worksheet.Cells[row, 2].Value = student.StudentCode;
                worksheet.Cells[row, 3].Value = student.FullName;
                worksheet.Cells[row, 4].Value = student.DateOfBirth.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 5].Value = student.Gender;
                worksheet.Cells[row, 6].Value = student.Address;
                worksheet.Cells[row, 7].Value = student.ParentPhone;
                worksheet.Cells[row, 8].Value = student.Email;
                row++;
            }

            worksheet.Cells.AutoFitColumns();
            return await package.GetAsByteArrayAsync();
        }

        public async Task<(List<Student> ValidStudents, List<ExcelErrorRow> ErrorRows)> ParseAndValidateImportFileAsync(Stream fileStream)
        {
            var validStudents = new List<Student>();
            var errorRows = new List<ExcelErrorRow>();

            using var package = new ExcelPackage(fileStream);
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension?.Rows ?? 0;

            if (rowCount <= 1)
            {
                return (validStudents, errorRows);
            }

            var currentDate = DateOnly.FromDateTime(DateTime.Today);

            for (int row = 2; row <= rowCount; row++)
            {
                var fullName = worksheet.Cells[row, 1].Text?.Trim();
                var dobText = worksheet.Cells[row, 2].Text?.Trim();
                var gender = worksheet.Cells[row, 3].Text?.Trim();
                var address = worksheet.Cells[row, 4].Text?.Trim();
                var parentPhone = worksheet.Cells[row, 5].Text?.Trim();
                var email = worksheet.Cells[row, 6].Text?.Trim();

                var errors = new List<string>();

                // BR-02: Validation logic
                if (string.IsNullOrEmpty(fullName))
                {
                    errors.Add("Họ tên không được trống.");
                }
                else if (!Regex.IsMatch(fullName, @"^[a-zA-Z\s\p{L}]+$"))
                {
                    errors.Add("Họ tên không hợp lệ (không chứa số, ký tự đặc biệt).");
                }

                DateOnly dob = default;
                if (string.IsNullOrEmpty(dobText))
                {
                    errors.Add("Ngày sinh không được trống.");
                }
                else if (!DateOnly.TryParseExact(dobText, "dd/MM/yyyy", out dob))
                {
                    errors.Add("Ngày sinh không đúng định dạng dd/MM/yyyy.");
                }
                else if (dob >= currentDate)
                {
                    errors.Add("Ngày sinh phải nhỏ hơn ngày hiện tại.");
                }

                if (string.IsNullOrEmpty(gender) || (gender != "Nam" && gender != "Nữ" && gender != "Khác"))
                {
                    errors.Add("Giới tính phải là 'Nam', 'Nữ' hoặc 'Khác'.");
                }

                if (string.IsNullOrEmpty(parentPhone))
                {
                    errors.Add("SĐT Phụ huynh không được trống.");
                }
                else if (!Regex.IsMatch(parentPhone, @"^(03|05|07|08|09)\d{8}$"))
                {
                    errors.Add("SĐT Phụ huynh phải có 10 chữ số và thuộc đầu số Việt Nam di động.");
                }

                if (!string.IsNullOrEmpty(email))
                {
                    var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                    if (!Regex.IsMatch(email, emailPattern))
                    {
                        errors.Add("Email không đúng định dạng.");
                    }
                }

                if (errors.Count > 0)
                {
                    errorRows.Add(new ExcelErrorRow
                    {
                        RowIndex = row,
                        FullName = fullName ?? "",
                        DateOfBirth = dobText ?? "",
                        Gender = gender ?? "",
                        Address = address ?? "",
                        ParentPhone = parentPhone ?? "",
                        Email = email ?? "",
                        ErrorMessage = string.Join("; ", errors)
                    });
                }
                else
                {
                    validStudents.Add(new Student
                    {
                        FullName = fullName!,
                        DateOfBirth = dob,
                        Gender = gender!,
                        Address = address,
                        ParentPhone = parentPhone!,
                        Email = email
                    });
                }
            }

            return await Task.FromResult((validStudents, errorRows));
        }

        public async Task<byte[]> GenerateErrorReportAsync(List<ExcelErrorRow> errorRows)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Danh sách lỗi");

            // Headers
            worksheet.Cells[1, 1].Value = "Dòng lỗi";
            worksheet.Cells[1, 2].Value = "Họ và tên";
            worksheet.Cells[1, 3].Value = "Ngày sinh";
            worksheet.Cells[1, 4].Value = "Giới tính";
            worksheet.Cells[1, 5].Value = "Địa chỉ";
            worksheet.Cells[1, 6].Value = "SĐT Phụ huynh";
            worksheet.Cells[1, 7].Value = "Email";
            worksheet.Cells[1, 8].Value = "Chi tiết lỗi";

            using (var range = worksheet.Cells[1, 1, 1, 8])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                range.Style.Font.Color.SetColor(System.Drawing.Color.White);
            }

            int rowIdx = 2;
            foreach (var error in errorRows)
            {
                worksheet.Cells[rowIdx, 1].Value = error.RowIndex;
                worksheet.Cells[rowIdx, 2].Value = error.FullName;
                worksheet.Cells[rowIdx, 3].Value = error.DateOfBirth;
                worksheet.Cells[rowIdx, 4].Value = error.Gender;
                worksheet.Cells[rowIdx, 5].Value = error.Address;
                worksheet.Cells[rowIdx, 6].Value = error.ParentPhone;
                worksheet.Cells[rowIdx, 7].Value = error.Email;
                worksheet.Cells[rowIdx, 8].Value = error.ErrorMessage;
                rowIdx++;
            }

            worksheet.Cells.AutoFitColumns();
            return await package.GetAsByteArrayAsync();
        }
    }
}