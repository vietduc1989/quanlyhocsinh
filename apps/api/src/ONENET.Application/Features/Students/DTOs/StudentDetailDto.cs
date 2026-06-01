// QUAN-20260530-2301
namespace ONENET.Application.Features.Students.DTOs;

public class StudentDetailDto
{
    public Guid Id { get; set; }
    public string MaHocSinh { get; set; } = default!;
    public string HoVaTen { get; set; } = default!;
    public DateTime NgaySinh { get; set; }
    public string GioiTinh { get; set; } = default!;
    public string? DiaChi { get; set; }
    public string? SdtPhuHuynh { get; set; }
    public string? EmailPhuHuynh { get; set; }
    public Guid LopId { get; set; }
    public string TenLop { get; set; } = default!;
    public DateTime NgayNhapHoc { get; set; }
    public Guid TrangThaiId { get; set; }
    public string TenTrangThai { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}