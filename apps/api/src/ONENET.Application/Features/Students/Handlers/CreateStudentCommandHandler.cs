<!-- QUAN-20260530-2301 -->
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Features.Students.Commands;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;

namespace ONENET.Application.Features.Students.Handlers
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILopRepository _lopRepository;
        private readonly ITrangThaiHocSinhRepository _trangThaiHocSinhRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<CreateStudentCommandHandler> _logger;

        public CreateStudentCommandHandler(
            IStudentRepository studentRepository,
            ILopRepository lopRepository,
            ITrangThaiHocSinhRepository trangThaiHocSinhRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            ILogger<CreateStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _lopRepository = lopRepository;
            _trangThaiHocSinhRepository = trangThaiHocSinhRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            // QUAN-20260530-2301-BR01: Mã Học sinh phải là duy nhất
            if (await _studentRepository.IsMaHocSinhUniqueAsync(request.MaHocSinh, null, cancellationToken))
            {
                _logger.LogWarning("Failed to create student: MaHocSinh '{MaHocSinh}' already exists.", request.MaHocSinh);
                throw new ValidationException(new FluentValidation.Results.ValidationFailure("MaHocSinh", $"Mã Học sinh '{request.MaHocSinh}' đã tồn tại. Vui lòng chọn mã khác."));
            }

            // QUAN-20260530-2301-BR02: Lớp Học là bắt buộc & phải tồn tại
            var lop = await _lopRepository.GetByIdAsync(request.LopId, cancellationToken);
            if (lop == null)
            {
                _logger.LogWarning("Failed to create student: LopId '{LopId}' not found.", request.LopId);
                throw new NotFoundException("Lop", request.LopId);
            }

            // QUAN-20260530-2301-BR02, QUAN-20260530-2301-BR06: Trạng Thái là bắt buộc & phải tồn tại
            var trangThai = await _trangThaiHocSinhRepository.GetByIdAsync(request.TrangThaiId, cancellationToken);
            if (trangThai == null)
            {
                _logger.LogWarning("Failed to create student: TrangThaiId '{TrangThaiId}' not found.", request.TrangThaiId);
                throw new NotFoundException("TrangThaiHocSinh", request.TrangThaiId);
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
                request.TrangThaiId,
                _currentUser.UserName ?? "system" // Use current user for audit
            );

            await _studentRepository.AddAsync(student, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Student created successfully: {StudentId} by {CreatedBy}", student.Id, student.CreatedBy);
            return student.Id;
        }
    }
}