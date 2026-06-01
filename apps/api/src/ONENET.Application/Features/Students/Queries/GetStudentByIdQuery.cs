using System;
using MediatR;
using ONENET.Application.Features.Students.Dtos;

namespace ONENET.Application.Features.Students.Queries
{
    public record GetStudentByIdQuery(Guid Id) : IRequest<StudentDetailDto>;
}
