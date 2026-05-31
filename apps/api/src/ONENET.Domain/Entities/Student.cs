// QUAN-20260530-2301
using ONENET.Domain.Common;
using System;

namespace ONENET.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string MaHocSinh { get; private set; } = default!;
        public string HoVaTen { get; private set; } = default!;
        public DateTime NgaySinh { get; private set; }
        public string GioiTinh { get; private set; } = default!;
        public string? DiaChi { get; private set; }
        public string? SDTPhuHuynh { get; private set; }
        public string? EmailPhuHuynh { get; private set; }
        public Guid LopId { get; private set; }
        public Lop Lop { get; private set; } = default!; // Navigation property
        public DateTime NgayNhapHoc { get; private set; }
        public Guid TrangThaiId { get; private set; }
        public TrangThaiHocSinh TrangThaiHocSinh { get; private set; } = default!; // Navigation property
        public int RowVersion { get; private set; } // Concurrency token

        // Private constructor for EF Core and internal entity creation
        private Student() { }

        // Factory method for creating a new Student
        public static Student Create(
            string maHocSinh, string hoVaTen, DateTime ngaySinh, string gioiTinh,
            string? diaChi, string? sdtPhuHuynh, string? emailPhuHuynh,
            Guid lopId, DateTime ngayNhapHoc, Guid trangThaiId)
        {
            return new Student
            {
                MaHocSinh = maHocSinh,
                HoVaTen = hoVaTen,
                NgaySinh = ngaySinh,
                GioiTinh = gioiTinh,
                DiaChi = diaChi,
                SDTPhuHuynh = sdtPhuHuynh,
                EmailPhuHuynh = emailPhuHuynh,
                LopId = lopId,
                NgayNhapHoc = ngayNhapHoc,
                TrangThaiId = trangThaiId
            };
        }

        // Method for updating student information
        public void Update(
            string? maHocSinh, string? hoVaTen, DateTime? ngaySinh, string? gioiTinh,
            string? diaChi, string? sdtPhuHuynh, string? emailPhuHuynh,
            Guid? lopId, DateTime? ngayNhapHoc, Guid? trangThaiId)
        {
            if (maHocSinh is not null && MaHocSinh != maHocSinh) MaHocSinh = maHocSinh;
            if (hoVaTen is not null && HoVaTen != hoVaTen) HoVaTen = hoVaTen;
            if (ngaySinh.HasValue && NgaySinh != ngaySinh.Value) NgaySinh = ngaySinh.Value;
            if (gioiTinh is not null && GioiTinh != gioiTinh) GioiTinh = gioiTinh;
            if (DiaChi != diaChi) DiaChi = diaChi;
            if (SDTPhuHuynh != sdtPhuHuynh) SDTPhuHuynh = sdtPhuHuynh;
            if (EmailPhuHuynh != emailPhuHuynh) EmailPhuHuynh = emailPhuHuynh;
            if (lopId.HasValue && LopId != lopId.Value) LopId = lopId.Value;
            if (ngayNhapHoc.HasValue && NgayNhapHoc != ngayNhapHoc.Value) NgayNhapHoc = ngayNhapHoc.Value;
            if (trangThaiId.HasValue && TrangThaiId != trangThaiId.Value) TrangThaiId = trangThaiId.Value;
        }
    }
}