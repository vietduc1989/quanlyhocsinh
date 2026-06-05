// QUAN-20260604-153038
using System;
using System.Collections.Generic;

namespace ONENET.Domain.Entities
{
    public class LopHoc : BaseEntity
    {
        // LopHocID in SRS is INT/UUID. Using Guid for consistency with BaseEntity.
        // If LopHocID in existing system is INT, this would need adjustment or a mapping layer.
        // For this project's purpose, we'll assume Guid.
        public string TenLop { get; set; } = string.Empty; // Unique
        public string Khoi { get; set; } = string.Empty;
        public int NamHoc { get; set; }

        // Navigation property for students in this class
        public ICollection<HocSinh> HocSinhs { get; set; } = new List<HocSinh>();
    }
}