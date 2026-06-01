// QUAN-20260530-2301
using ONENET.Domain.Common;

namespace ONENET.Domain.Entities;

public class Lop : BaseEntity
{
    public string TenLop { get; private set; } = default!;
    public string? MoTa { get; private set; }

    // Private constructor for EF Core and domain logic control
    private Lop() { }

    public Lop(string tenLop, string? moTa)
    {
        TenLop = tenLop;
        MoTa = moTa;
    }

    public void Update(string tenLop, string? moTa)
    {
        TenLop = tenLop;
        MoTa = moTa;
        UpdatedAt = DateTime.UtcNow;
    }
}