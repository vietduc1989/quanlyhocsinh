// QUAN-20260530-2301
using MediatR;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Common.Models;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;

namespace ONENET.Application.Features.Students.Handlers;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, ApiResponse<Guid>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILopRepository _lopRepository;
    private readonly ITrangThaiHocSinhRepository _trangThaiRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public CreateStudentCommandHandler(
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

    public async Task<ApiResponse<Guid>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        // QUAN-20260530-2301-BR01: Mã Học sinh duy nhất
        if (await _studentRepository.ExistsByMaHocSinhAsync(request.MaHocSinh, null, cancellationToken))
        {
            throw new ValidationException(new List<FluentValidation.Results.ValidationFailure>
            {
                new("MaHocSinh", $"Mã Học sinh '{request.MaHocSinh}' đã tồn tại. Vui lòng chọn mã khác.")
            });
        }

        // QUAN-20260530-2301-BR02 & BR06: Lớp học và Trạng thái phải tồn tại
        var lopExists = await _lopRepository.GetByIdAsync(request.LopId, cancellationToken);
        if (lopExists is null)
        {
            throw new NotFoundException(new List<ApiError>
            {
                new() { Field = "LopId", Message = $"Lớp học với ID '{request.LopId}' không tồn tại." }
            });
        }

        var trangThaiExists = await _trangThaiRepository.GetByIdAsync(request.TrangThaiId, cancellationToken);
        if (trangThaiExists is null)
        {
            throw new NotFoundException(new List<ApiError>
            {
                new() { Field = "TrangThaiId", Message = $"Trạng thái học sinh với ID '{request.TrangThaiId}' không tồn tại." }
            });
        }

        var student = Student.Create(
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

        student.CreatedBy = _currentUser.UserName ?? _currentUser.UserId?.ToString();

        await _studentRepository.AddAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<Guid>.SuccessResult(student.Id, "Thêm học sinh thành công");
    }
}