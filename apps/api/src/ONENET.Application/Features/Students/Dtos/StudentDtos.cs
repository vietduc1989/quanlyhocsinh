// QUAN-20260530-2301
using System;
using System.Collections.Generic;

namespace ONENET.Application.Features.Students.Dtos
{
    // DTOs for lookup tables
    public record LopDto(Guid Id, string TenLop);
    public record TrangThaiHocSinhDto(Guid Id, string MaTrangThai, string TenTrangThai);

    // Request DTOs
    public record CreateStudentDto(
        string MaHocSinh,
        string HoVaTen,
        DateTime NgaySinh,
        string GioiTinh,
        string? DiaChi,
        string? SDTPhuHuynh,
        string? EmailPhuHuynh,
        Guid LopId,
        DateTime NgayNhapHoc,
        Guid TrangThaiId
    );

    public record UpdateStudentDto(
        string? MaHocSinh,
        string? HoVaTen,
        DateTime? NgaySinh,
        string? GioiTinh,
        string? DiaChi,
        string? SDTPhuHuynh,
        string? EmailPhuHuynh,
        Guid? LopId,
        DateTime? NgayNhapHoc,
        Guid? TrangThaiId
    );

    // Response DTOs
    public record StudentIdDto(Guid Id);

    public record StudentSummaryDto(
        Guid Id,
        string MaHocSinh,
        string HoVaTen,
        string TenLop,
        string TenTrangThai
    );

    public record StudentDetailDto(
        Guid Id,
        string MaHocSinh,
        string HoVaTen,
        DateTime NgaySinh,
        string GioiTinh,
        string? DiaChi,
        string? SDTPhuHuynh,
        string? EmailPhuHuynh,
        Guid LopId,
        string TenLop,
        DateTime NgayNhapHoc,
        Guid TrangThaiId,
        string TenTrangThai,
        DateTime NgayTao,
        string? NguoiTao,
        DateTime? NgayCapNhat,
        string? NguoiCapNhat
    );

    public record SuccessDto(bool Success);
}