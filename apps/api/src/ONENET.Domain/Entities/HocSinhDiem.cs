// QUAN-20260604-153038
// Placeholder entity to simulate related data for FR06 (prevent deletion if related data exists).
// This entity is not explicitly detailed in the BRD/SRS but is needed to implement FR06.
using System;

namespace ONENET.Domain.Entities
{
    public class HocSinhDiem : BaseEntity
    {
        public Guid HocSinhId { get; set; }
        public HocSinh HocSinh { get; set; } = default!;

        public string MonHoc { get; set; } = string.Empty;
        public decimal DiemSo { get; set; }
        public int HocKy { get; set; }
        public int NamHoc { get; set; }
    }
}