// QUAN-20260530-2301
using ONENET.Domain.Common;
using System;
using System.Collections.Generic;

namespace ONENET.Domain.Entities
{
    public class TrangThaiHocSinh : BaseEntity
    {
        public string MaTrangThai { get; private set; } = default!;
        public string TenTrangThai { get; private set; } = default!;
        public string? MoTa { get; private set; }

        public ICollection<Student> Students { get; private set; } = new HashSet<Student>(); // Navigation property

        private TrangThaiHocSinh() { } // Private constructor for EF Core

        public static TrangThaiHocSinh Create(string maTrangThai, string tenTrangThai, string? moTa)
        {
            return new TrangThaiHocSinh { MaTrangThai = maTrangThai, TenTrangThai = tenTrangThai, MoTa = moTa };
        }
    }
}