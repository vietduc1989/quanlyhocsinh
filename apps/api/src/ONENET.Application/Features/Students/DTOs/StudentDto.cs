<!-- QUAN-20260530-2301 -->
using System;

namespace ONENET.Application.Features.Students.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string MaHocSinh { get; set; } = string.Empty;
        public string HoVaTen { get; set; } = string.Empty;
        public string LopHoc { get; set; } = string.Empty; // TenLop
        public string TrangThai { get; set; } = string.Empty; // TenTrangThai
    }
}