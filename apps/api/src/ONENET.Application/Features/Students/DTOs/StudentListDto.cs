// QUAN-20260530-2301
namespace ONENET.Application.Features.Students.DTOs;

public class StudentListDto
{
    public Guid Id { get; set; }
    public string MaHocSinh { get; set; } = default!;
    public string HoVaTen { get; set; } = default!;
    public string TenLop { get; set; } = default!;
    public string TenTrangThai { get; set; } = default!;
}