<!-- QUAN-20260530-2301 -->
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Features.Students.Commands;
using ONENET.Domain.Interfaces;

namespace ONENET.Application.Features.Students.Handlers
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Unit>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILopRepository _lopRepository;
        private readonly ITrangThaiHocSinhRepository _trangThaiHocSinhRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<UpdateStudentCommandHandler> _logger;

        public UpdateStudentCommandHandler(
            IStudentRepository studentRepository,
            ILopRepository lopRepository,
            ITrangThaiHocSinhRepository trangThaiHocSinhRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            ILogger<UpdateStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _lopRepository = lopRepository;
            _trangThaiHocSinhRepository = trangThaiHocSinhRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (student == null)
            {
                _logger.LogWarning("Failed to update student: Student with ID '{StudentId}' not found.", request.Id);
                throw new NotFoundException("Student", request.Id);
            }

            // QUAN-20260530-2301-BR01: Mã Học sinh phải là duy nhất (nếu được cập nhật)
            if (!string.IsNullOrEmpty(request.MaHocSinh) && request.MaHocSinh != student.MaHocSinh)
            {
                if (await _studentRepository.IsMaHocSinhUniqueAsync(request.MaHocSinh, request.Id, cancellationToken))
                {
                    _logger.LogWarning("Failed to update student {StudentId}: MaHocSinh '{MaHocSinh}' already exists for another student.", request.Id, request.MaHocSinh);
                    throw new ValidationException(new FluentValidation.Results.ValidationFailure("MaHocSinh", $"Mã Học sinh '{request.MaHocSinh}' đã được sử dụng bởi học sinh khác."));
                }
            }

            // Check if LopId is updated and exists
            if (request.LopId.HasValue && request.LopId != student.LopId)
            {
                var lop = await _lopRepository.GetByIdAsync(request.LopId.Value, cancellationToken);
                if (lop == null)
                {
                    _logger.LogWarning("Failed to update student {StudentId}: LopId '{LopId}' not found.", request.Id, request.LopId);
                    throw new NotFoundException("Lop", request.LopId.Value);
                }
            }

            // Check if TrangThaiId is updated and exists
            if (request.TrangThaiId.HasValue && request.TrangThaiId != student.TrangThaiId)
            {
                var trangThai = await _trangThaiHocSinhRepository.GetByIdAsync(request.TrangThaiId.Value, cancellationToken);
                if (trangThai == null)
                {
                    _logger.LogWarning("Failed to update student {StudentId}: TrangThaiId '{TrangThaiId}' not found.", request.Id, request.TrangThaiId);
                    throw new NotFoundException("TrangThaiHocSinh", request.TrangThaiId.Value);
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
                request.TrangThaiId,
                _currentUser.UserName ?? "system" // Use current user for audit
            );

            _studentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Student updated successfully: {StudentId} by {UpdatedBy}", request.Id, _currentUser.UserName);
            return Unit.Value;
        }
    }
}