// QUAN-20260530-2301
using MediatR;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Common.Models;
using ONENET.Domain.Interfaces;

namespace ONENET.Application.Features.Students.Handlers;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, ApiResponse>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILopRepository _lopRepository;
    private readonly ITrangThaiHocSinhRepository _trangThaiRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public UpdateStudentCommandHandler(
        IStudentRepository studentRepository,
        ILopRepository lopRepository,
        ITrangThaiHocSinhRepository trangThaiRepository,
        IUnitOfWork unitOfWork,
        ICurrentUser currentUser)
    {
        _studentRepository = studentRepository;
        _lopRepository = lopRepository;
        _trangThaiRepository = trangThaiRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<ApiResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (student is null)
        {
            throw new NotFoundException($"Học sinh không tìm thấy với ID: {request.Id}");
        }

        // QUAN-20260530-2301-BR01: Mã Học sinh duy nhất (nếu thay đổi)
        if (!string.IsNullOrWhiteSpace(request.MaHocSinh) && request.MaHocSinh != student.MaHocSinh)
        {
            if (await _studentRepository.ExistsByMaHocSinhAsync(request.MaHocSinh, request.Id, cancellationToken))
            {
                throw new ValidationException(new List<FluentValidation.Results.ValidationFailure>
                {
                    new("MaHocSinh", $"Mã Học sinh '{request.MaHocSinh}' đã được sử dụng bởi học sinh khác.")
                });
            }
        }

        // QUAN-20260530-2301-BR02 & BR06: Lớp học và Trạng thái phải tồn tại (nếu thay đổi)
        if (request.LopId.HasValue && request.LopId != student.LopId)
        {
            var lopExists = await _lopRepository.GetByIdAsync(request.LopId.Value, cancellationToken);
            if (lopExists is null)
            {
                throw new NotFoundException(new List<ApiError>
                {
                    new() { Field = "LopId", Message = $"Lớp học với ID '{request.LopId.Value}' không tồn tại." }
                });
            }
        }

        if (request.TrangThaiId.HasValue && request.TrangThaiId != student.TrangThaiId)
        {
            var trangThaiExists = await _trangThaiRepository.GetByIdAsync(request.TrangThaiId.Value, cancellationToken);
            if (trangThaiExists is null)
            {
                throw new NotFoundException(new List<ApiError>
                {
                    new() { Field = "TrangThaiId", Message = $"Trạng thái học sinh với ID '{request.TrangThaiId.Value}' không tồn tại." }
                });
            }
        }

        student.Update(
            request.MaHocSinh,
            request.HoVaTen,
            request.NgaySinh,
            request.GioiTinh,
            request.DiaChi,
            request.SdtPhuHuynh,
            request.EmailPhuHuynh,
            request.LopId,
            request.NgayNhapHoc,
            request.TrangThaiId);

        student.UpdatedBy = _currentUser.UserName ?? _currentUser.UserId?.ToString();

        _studentRepository.Update(student);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.SuccessResult("Cập nhật học sinh thành công");
    }
}