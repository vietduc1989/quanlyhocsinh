using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ONENET.Application.Students.Dtos;
using ONENET.Domain.Repositories;

namespace ONENET.Application.Students.Queries.GetStudentById;

public record GetStudentByIdQuery(Guid Id) : IRequest<StudentDto?>;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDto?>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentByIdQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (student == null) return null;

        return new StudentDto
        {
            Id = student.Id,
            StudentCode = student.StudentCode,
            FullName = student.FullName,
            DateOfBirth = student.DateOfBirth,
            Gender = student.Gender,
            Address = student.Address,
            PhoneNumber = student.PhoneNumber,
            Email = student.Email,
            ClassId = student.ClassId,
            ClassName = student.Class?.ClassName ?? "Chưa phân lớp",
            ParentName = student.ParentName,
            ParentPhoneNumber = student.ParentPhoneNumber,
            Status = student.Status
        };
    }
}