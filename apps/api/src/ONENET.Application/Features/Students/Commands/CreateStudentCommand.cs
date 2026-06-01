using System;
using MediatR;
using ONENET.Domain.Enums;

namespace ONENET.Application.Features.Students.Commands
{
    public record CreateStudentCommand(
        string MaHocSinh,
        string HoVaTen,
        DateTime NgaySinh,
        GioiTinh GioiTinh,
        string? DiaChi,
        string? SdtPhuHuynh,
        string? EmailPhuHuynh,
        Guid LopId,
        DateTime NgayNhapHoc,
        Guid TrangThaiId
    ) : IRequest<ONENET.Application.Features.Students.Dtos.StudentIdDto>;
}