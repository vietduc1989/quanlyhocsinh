<!-- QUAN-20260530-2301 -->
using System;
using ONENET.Domain.Enums;

namespace ONENET.Application.Features.Students.DTOs
{
    public class StudentDetailDto
    {
        public Guid Id { get; set; }
        public string MaHocSinh { get; set; } = string.Empty;
        public string HoVaTen { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; } = string.Empty; // Converted to string for display
        public string? DiaChi { get; set; }
        public string? SdtPhuHuynh { get; set; }
        public string? EmailPhuHuynh { get; set; }
        public Guid LopId { get; set; }
        public string TenLop { get; set; } = string.Empty;
        public DateTime NgayNhapHoc { get; set; }
        public Guid TrangThaiId { get; set; }
        public string TenTrangThai { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; }
        public string? NguoiTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string? NguoiCapNhat { get; set; }
    }
}