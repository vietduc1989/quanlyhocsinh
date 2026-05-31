using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ONENET.Application.Students.Dtos;
using ONENET.Domain.Entities;
using ONENET.Domain.Repositories;

namespace ONENET.Application.Students.Commands.CreateStudent;

/// <summary>
/// Command định nghĩa dữ liệu yêu cầu thêm học sinh mới.
/// </summary>
public record CreateStudentCommand : IRequest<Guid>
{
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
}

/// <summary>
/// Handler xử lý logic thêm mới học sinh, tuân thủ CQRS.
/// </summary>
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IStudentRepository _studentRepository;

    public CreateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        // 1. Kiểm tra tính duy nhất của Mã học sinh
        var isUnique = await _studentRepository.IsCodeUniqueAsync(request.StudentCode, null, cancellationToken);
        if (!isUnique)
        {
            throw new InvalidOperationException($"Mã Học sinh '{request.StudentCode}' đã tồn tại trong hệ thống.");
        }

        // 2. Map dữ liệu sang Domain Entity
        var student = new Student
        {
            Id = Guid.NewGuid(),
            StudentCode = request.StudentCode,
            FullName = request.FullName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            ClassId = request.ClassId,
            ParentName = request.ParentName,
            ParentPhoneNumber = request.ParentPhoneNumber,
            Status = Domain.Enums.StudentStatus.Active,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "System_Dev" // Sẽ lấy từ User Context sau khi tích hợp JWT
        };

        // 3. Persist dữ liệu qua Repository
        await _studentRepository.AddAsync(student, cancellationToken);
        await _studentRepository.SaveChangesAsync(cancellationToken);

        return student.Id;
    }
}