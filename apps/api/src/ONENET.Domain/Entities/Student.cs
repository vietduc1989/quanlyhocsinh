// QUAN-20260530-2301
using ONENET.Domain.Common;

namespace ONENET.Domain.Entities;

public class Student : BaseEntity
{
    public string MaHocSinh { get; private set; } = default!;
    public string HoVaTen { get; private set; } = default!;
    public DateTime NgaySinh { get; private set; }
    public string GioiTinh { get; private set; } = default!; // "Nam", "Nữ", "Khác"
    public string? DiaChi { get; private set; }
    public string? SdtPhuHuynh { get; private set; }
    public string? EmailPhuHuynh { get; private set; }
    public Guid LopId { get; private set; }
    public DateTime NgayNhapHoc { get; private set; }
    public Guid TrangThaiId { get; private set; }

    public uint RowVersion { get; private set; } // Concurrency token for PostgreSQL xmin

    // Navigation properties
    public Lop Lop { get; private set; } = default!;
    public TrangThaiHocSinh TrangThaiHocSinh { get; private set; } = default!;

    // Private constructor for EF Core
    private Student() { }

    public static Student Create(
        string maHocSinh,
        string hoVaTen,
        DateTime ngaySinh,
        string gioiTinh,
        string? diaChi,
        string? sdtPhuHuynh,
        string? emailPhuHuynh,
        Guid lopId,
        DateTime ngayNhapHoc,
        Guid trangThaiId)
    {
        return new Student
        {
            MaHocSinh = maHocSinh,
            HoVaTen = hoVaTen,
            NgaySinh = ngaySinh,
            GioiTinh = gioiTinh,
            DiaChi = diaChi,
            SdtPhuHuynh = sdtPhuHuynh,
            EmailPhuHuynh = emailPhuHuynh,
            LopId = lopId,
            NgayNhapHoc = ngayNhapHoc,
            TrangThaiId = trangThaiId
        };
    }

    public void Update(
        string? maHocSinh,
        string? hoVaTen,
        DateTime? ngaySinh,
        string? gioiTinh,
        string? diaChi,
        string? sdtPhuHuynh,
        string? emailPhuHuynh,
        Guid? lopId,
        DateTime? ngayNhapHoc,
        Guid? trangThaiId)
    {
        MaHocSinh = maHocSinh ?? MaHocSinh;
        HoVaTen = hoVaTen ?? HoVaTen;
        NgaySinh = ngaySinh ?? NgaySinh;
        GioiTinh = gioiTinh ?? GioiTinh;
        DiaChi = diaChi ?? DiaChi;
        SdtPhuHuynh = sdtPhuHuynh ?? SdtPhuHuynh;
        EmailPhuHuynh = emailPhuHuynh ?? EmailPhuHuynh;
        LopId = lopId ?? LopId;
        NgayNhapHoc = ngayNhapHoc ?? NgayNhapHoc;
        TrangThaiId = trangThaiId ?? TrangThaiId;
        UpdatedAt = DateTime.UtcNow;
    }
}