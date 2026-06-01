<!-- QUAN-20260530-2301 -->
using System;
using ONENET.Domain.Enums;

namespace ONENET.Application.Features.Students.DTOs
{
    public class UpdateStudentDto
    {
        public string? MaHocSinh { get; set; }
        public string? HoVaTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public GioiTinh? GioiTinh { get; set; }
        public string? DiaChi { get; set; }
        public string? SdtPhuHuynh { get; set; }
        public string? EmailPhuHuynh { get; set; }
        public Guid? LopId { get; set; }
        public DateTime? NgayNhapHoc { get; set; }
        public Guid? TrangThaiId { get; set; }
    }
}