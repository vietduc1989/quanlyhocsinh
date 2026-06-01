using System;
using System.Collections.Generic;

namespace ONENET.Application.Features.Students.Dtos
{
    // DTOs for lookup tables
    public record LopDto(Guid Id, string TenLop);
    public record TrangThaiHocSinhDto(Guid Id, string MaTrangThai, string TenTrangThai);

    // Response DTOs
    public record StudentIdDto(Guid Id);

    public record StudentSummaryDto(
        Guid Id,
        string MaHocSinh,
        string HoVaTen,
        string TenLop,
        string TenTrangThai
    );

    public record SuccessDto(bool Success);
}
