// QUAN-20260530-2301
using ONENET.Domain.Common;

namespace ONENET.Domain.Entities;

public class TrangThaiHocSinh : BaseEntity
{
    public string MaTrangThai { get; private set; } = default!;
    public string TenTrangThai { get; private set; } = default!;
    public string? MoTa { get; private set; }

    // Private constructor for EF Core and domain logic control
    private TrangThaiHocSinh() { }

    public TrangThaiHocSinh(string maTrangThai, string tenTrangThai, string? moTa)
    {
        MaTrangThai = maTrangThai;
        TenTrangThai = tenTrangThai;
        MoTa = moTa;
    }

    public void Update(string maTrangThai, string tenTrangThai, string? moTa)
    {
        MaTrangThai = maTrangThai;
        TenTrangThai = tenTrangThai;
        MoTa = moTa;
        UpdatedAt = DateTime.UtcNow;
    }
}