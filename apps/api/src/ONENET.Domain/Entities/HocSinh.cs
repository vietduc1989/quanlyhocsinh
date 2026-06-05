// QUAN-20260604-153038
using ONENET.Domain.Enums;
using System;

namespace ONENET.Domain.Entities
{
    public class HocSinh : BaseEntity
    {
        public string MaHocSinh { get; set; } = string.Empty; // BR01: Unique, auto-generated
        public string HoTen { get; set; } = string.Empty; // BR02: Required
        public DateOnly NgaySinh { get; set; } // BR02, BR03: Required, valid date, not in future
        public GioiTinh GioiTinh { get; set; } // BR02: Required
        public string? DiaChi { get; set; }
        public string? SoDienThoaiPH { get; set; } // BR04: Valid format
        public string? EmailPH { get; set; } // BR05: Valid format

        // Foreign Key to LopHoc
        public Guid LopHocId { get; set; } // BR02: Required
        public LopHoc LopHoc { get; set; } = default!; // Navigation property

        public TrangThaiHocSinh TrangThai { get; set; } // BR02, BR06: Required, predefined values
    }
}