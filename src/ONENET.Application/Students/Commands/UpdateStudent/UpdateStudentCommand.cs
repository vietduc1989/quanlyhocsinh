using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ONENET.Domain.Enums;
using ONENET.Domain.Repositories;

namespace ONENET.Application.Students.Commands.UpdateStudent;

public record UpdateStudentCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string StudentCode { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public string Gender { get; init; } = string.Empty;
    public string? Address { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Email { get; init; }
    public Guid ClassId { get; init; }
    public string? ParentName { get; init; }
    public string? ParentPhoneNumber { get; init; }
    public StudentStatus Status { get; init; }
}

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Unit>
{
    private readonly IStudentRepository _studentRepository;

    public UpdateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (student == null)
        {
            throw new KeyNotFoundException($"Không tìm thấy học sinh với ID: {request.Id}");
        }

        // Kiểm tra trùng mã học sinh khi đổi mã
        var isUnique = await _studentRepository.IsCodeUniqueAsync(request.StudentCode, request.Id, cancellationToken);
        if (!isUnique)
        {
            throw new InvalidOperationException($"Mã Học sinh '{request.StudentCode}' đã được sử dụng bởi học sinh khác.");
        }

        // Cập nhật thông tin thực thể
        student.StudentCode = request.StudentCode;
        student.FullName = request.FullName;
        student.DateOfBirth = request.DateOfBirth;
        student.Gender = request.Gender;
        student.Address = request.Address;
        student.PhoneNumber = request.PhoneNumber;
        student.Email = request.Email;
        student.ClassId = request.ClassId;
        student.ParentName = request.ParentName;
        student.ParentPhoneNumber = request.ParentPhoneNumber;
        student.Status = request.Status;
        student.LastModifiedAt = DateTime.UtcNow;
        student.LastModifiedBy = "System_Dev";

        _studentRepository.Update(student);
        await _studentRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}