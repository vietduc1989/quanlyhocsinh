using System.Collections.Generic;
using ONENET.Domain.Common;

namespace ONENET.Domain.Entities
{
    public class TrangThaiHocSinh : BaseEntity
    {
        public string MaTrangThai { get; set; } = string.Empty;
        public string TenTrangThai { get; set; } = string.Empty;
        public string? MoTa { get; set; }

        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
