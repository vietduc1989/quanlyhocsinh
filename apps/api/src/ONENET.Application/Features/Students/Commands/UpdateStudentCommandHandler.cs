// QUAN-20260530-2301
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Features.Students.Dtos;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.Students.Commands
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, SuccessDto>
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

        public async Task<SuccessDto> Handle(UpdateStudentCommand request, CancellationToken ct)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, ct);
            if (student == null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }

            var studentDto = request.Student;

            // QUAN-20260530-2301-BR01: Mã Học sinh duy nhất (nếu thay đổi)
            if (studentDto.MaHocSinh is not null && student.MaHocSinh != studentDto.MaHocSinh)
            {
                if (await _studentRepository.ExistsByMaHocSinhAsync(studentDto.MaHocSinh, student.Id, ct))
                {
                    throw new ValidationException(nameof(studentDto.MaHocSinh), $"Mã Học sinh '{studentDto.MaHocSinh}' đã được sử dụng bởi học sinh khác.");
                }
            }

            // Check if LopId exists if it's being updated
            if (studentDto.LopId.HasValue && student.LopId != studentDto.LopId.Value)
            {
                var lop = await _lopRepository.GetByIdAsync(studentDto.LopId.Value, ct);
                if (lop == null)
                {
                    throw new NotFoundException(nameof(Lop), studentDto.LopId.Value);
                }
            }

            // Check if TrangThaiId exists if it's being updated
            if (studentDto.TrangThaiId.HasValue && student.TrangThaiId != studentDto.TrangThaiId.Value)
            {
                var trangThai = await _trangThaiHocSinhRepository.GetByIdAsync(studentDto.TrangThaiId.Value, ct);
                if (trangThai == null)
                {
                    throw new NotFoundException(nameof(TrangThaiHocSinh), studentDto.TrangThaiId.Value);
                }
            }

            student.Update(
                studentDto.MaHocSinh,
                studentDto.HoVaTen,
                studentDto.NgaySinh,
                studentDto.GioiTinh,
                studentDto.DiaChi,
                studentDto.SDTPhuHuynh,
                studentDto.EmailPhuHuynh,
                studentDto.LopId,
                studentDto.NgayNhapHoc,
                studentDto.TrangThaiId
            );

            student.UpdatedBy = _currentUser.UserId;
            student.UpdatedAt = DateTime.UtcNow;

            _studentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync(ct);

            _logger.LogInformation("Student updated: {StudentId} by {UserId}", student.Id, _currentUser.UserId);

            return new SuccessDto(true);
        }
    }
}