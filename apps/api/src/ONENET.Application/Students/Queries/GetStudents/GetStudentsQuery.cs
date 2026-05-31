using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ONENET.Application.Students.Dtos;
using ONENET.Domain.Repositories;

namespace ONENET.Application.Students.Queries.GetStudents;

public record GetStudentsQuery : IRequest<PagedListDto<StudentDto>>
{
    public string? SearchTerm { get; init; }
    public Guid? ClassId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, PagedListDto<StudentDto>>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentsQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<PagedListDto<StudentDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _studentRepository.GetPagedListAsync(
            request.SearchTerm,
            request.ClassId,
            request.PageNumber,
            request.PageSize,
            cancellationToken
        );

        var dtos = items.Select(student => new StudentDto
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
        }).ToList();

        return new PagedListDto<StudentDto>(dtos, totalCount, request.PageNumber, request.PageSize);
    }
}

public class PagedListDto<T>
{
    public List<T> Items { get; }
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public PagedListDto(List<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}