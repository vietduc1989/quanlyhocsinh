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
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentIdDto>
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

        public async Task<StudentIdDto> Handle(CreateStudentCommand request, CancellationToken ct)
        {
            var studentDto = request;

            // QUAN-20260530-2301-BR01: Mã Học sinh duy nhất
            if (!await _studentRepository.IsMaHocSinhUniqueAsync(studentDto.MaHocSinh, null, ct))
            {
                throw new ValidationException(new[] { new FluentValidation.Results.ValidationFailure(nameof(studentDto.MaHocSinh), $"Mã Học sinh '{studentDto.MaHocSinh}' đã tồn tại. Vui lòng chọn mã khác.") });
            }

            // Check if LopId exists
            var lop = await _lopRepository.GetByIdAsync(studentDto.LopId, ct);
            if (lop == null)
            {
                throw new NotFoundException(nameof(Lop), studentDto.LopId);
            }

            // Check if TrangThaiId exists
            var trangThai = await _trangThaiHocSinhRepository.GetByIdAsync(studentDto.TrangThaiId, ct);
            if (trangThai == null)
            {
                throw new NotFoundException(nameof(TrangThaiHocSinh), studentDto.TrangThaiId);
            }

            var student = Student.Create(
                studentDto.MaHocSinh,
                studentDto.HoVaTen,
                studentDto.NgaySinh,
                studentDto.GioiTinh,
                studentDto.DiaChi,
                studentDto.SdtPhuHuynh,
                studentDto.EmailPhuHuynh,
                studentDto.LopId,
                studentDto.NgayNhapHoc,
                studentDto.TrangThaiId,
                _currentUser.UserId ?? "System"
            );

            await _studentRepository.AddAsync(student, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            _logger.LogInformation("Student created: {StudentId} by {UserId}", student.Id, _currentUser.UserId);

            return new StudentIdDto(student.Id);
        }
    }
}
