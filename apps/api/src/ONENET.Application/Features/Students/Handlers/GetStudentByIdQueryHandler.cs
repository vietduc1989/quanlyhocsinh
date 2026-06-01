// QUAN-20260530-2301
using MediatR;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.Students.DTOs;
using ONENET.Domain.Interfaces;

namespace ONENET.Application.Features.Students.Handlers;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, ApiResponse<StudentDetailDto>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILopRepository _lopRepository;
    private readonly ITrangThaiHocSinhRepository _trangThaiRepository;

    public GetStudentByIdQueryHandler(
        IStudentRepository studentRepository,
        ILopRepository lopRepository,
        ITrangThaiHocSinhRepository trangThaiRepository)
    {
        _studentRepository = studentRepository;
        _lopRepository = lopRepository;
        _trangThaiRepository = trangThaiRepository;
    }

    public async Task<ApiResponse<StudentDetailDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (student is null)
        {
            throw new NotFoundException($"Học sinh không tìm thấy với ID: {request.Id}");
        }

        // Fetch related lookup data
        var lop = await _lopRepository.GetByIdAsync(student.LopId, cancellationToken);
        var trangThai = await _trangThaiRepository.GetByIdAsync(student.TrangThaiId, cancellationToken);

        var studentDetail = new StudentDetailDto
        {
            Id = student.Id,
            MaHocSinh = student.MaHocSinh,
            HoVaTen = student.HoVaTen,
            NgaySinh = student.NgaySinh,
            GioiTinh = student.GioiTinh,
            DiaChi = student.DiaChi,
            SdtPhuHuynh = student.SdtPhuHuynh,
            EmailPhuHuynh = student.EmailPhuHuynh,
            LopId = student.LopId,
            TenLop = lop?.TenLop ?? "Không xác định",
            NgayNhapHoc = student.NgayNhapHoc,
            TrangThaiId = student.TrangThaiId,
            TenTrangThai = trangThai?.TenTrangThai ?? "Không xác định",
            CreatedAt = student.CreatedAt,
            CreatedBy = student.CreatedBy,
            UpdatedAt = student.UpdatedAt,
            UpdatedBy = student.UpdatedBy
        };

        return ApiResponse<StudentDetailDto>.SuccessResult(studentDetail, "Chi tiết học sinh");
    }
}