// QUAN-20260530-2301
using MediatR;
using ONENET.Application.Common.Models;

namespace ONENET.Application.Features.Students.Commands;

public record CreateStudentCommand(
    string MaHocSinh,
    string HoVaTen,
    DateTime NgaySinh,
    string GioiTinh,
    string? DiaChi,
    string? SdtPhuHuynh,
    string? EmailPhuHuynh,
    Guid LopId,
    DateTime NgayNhapHoc,
    Guid TrangThaiId) : IRequest<ApiResponse<Guid>>;