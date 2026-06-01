using System;
using ONENET.Domain.Common;
using ONENET.Domain.Enums;

namespace ONENET.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string MaHocSinh { get; set; } = string.Empty;
        public string HoVaTen { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; } // Stored as DATE
        public GioiTinh GioiTinh { get; set; }
        public string? DiaChi { get; set; }
        public string? SdtPhuHuynh { get; set; }
        public string? EmailPhuHuynh { get; set; }
        public Guid LopId { get; set; }
        public Lop Lop { get; set; } = default!;
        public DateTime NgayNhapHoc { get; set; } // Stored as DATE
        public Guid TrangThaiId { get; set; }
        public TrangThaiHocSinh TrangThai { get; set; } = default!;

        // For Concurrency Control (xmin in PostgreSQL)
        public uint RowVersion { get; set; }

        public static Student Create(
            string maHocSinh,
            string hoVaTen,
            DateTime ngaySinh,
            GioiTinh gioiTinh,
            string? diaChi,
            string? sdtPhuHuynh,
            string? emailPhuHuynh,
            Guid lopId,
            DateTime ngayNhapHoc,
            Guid trangThaiId,
            string createdBy)
        {
            return new Student
            {
                MaHocSinh = maHocSinh,
                HoVaTen = hoVaTen,
                NgaySinh = ngaySinh.Date,
                GioiTinh = gioiTinh,
                DiaChi = diaChi,
                SdtPhuHuynh = sdtPhuHuynh,
                EmailPhuHuynh = emailPhuHuynh,
                LopId = lopId,
                NgayNhapHoc = ngayNhapHoc.Date,
                TrangThaiId = trangThaiId,
                CreatedBy = createdBy,
                UpdatedAt = DateTime.UtcNow, // Initial update time
                UpdatedBy = createdBy // Initial update user
            };
        }

        public void Update(
            string? maHocSinh,
            string? hoVaTen,
            DateTime? ngaySinh,
            GioiTinh? gioiTinh,
            string? diaChi,
            string? sdtPhuHuynh,
            string? emailPhuHuynh,
            Guid? lopId,
            DateTime? ngayNhapHoc,
            Guid? trangThaiId,
            string updatedBy)
        {
            MaHocSinh = maHocSinh ?? MaHocSinh;
            HoVaTen = hoVaTen ?? HoVaTen;
            NgaySinh = ngaySinh?.Date ?? NgaySinh;
            GioiTinh = gioiTinh ?? GioiTinh;
            DiaChi = diaChi ?? DiaChi;
            SdtPhuHuynh = sdtPhuHuynh ?? SdtPhuHuynh;
            EmailPhuHuynh = emailPhuHuynh ?? EmailPhuHuynh;
            LopId = lopId ?? LopId;
            NgayNhapHoc = ngayNhapHoc?.Date ?? NgayNhapHoc;
            TrangThaiId = trangThaiId ?? TrangThaiId;

            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }
    }
}
