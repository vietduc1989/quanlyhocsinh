// QUAN-20260530-2301
using ONENET.Domain.Common;
using System;
using System.Collections.Generic;

namespace ONENET.Domain.Entities
{
    public class Lop : BaseEntity
    {
        public string TenLop { get; private set; } = default!;
        public string? MoTa { get; private set; }

        public ICollection<Student> Students { get; private set; } = new HashSet<Student>(); // Navigation property

        private Lop() { } // Private constructor for EF Core

        public static Lop Create(string tenLop, string? moTa)
        {
            return new Lop { TenLop = tenLop, MoTa = moTa };
        }
    }
}