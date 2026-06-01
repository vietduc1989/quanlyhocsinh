<!-- QUAN-20260530-2301 -->
using System;
using MediatR;
using ONENET.Application.Features.Students.DTOs;

namespace ONENET.Application.Features.Students.Queries
{
    public record GetStudentByIdQuery(Guid Id) : IRequest<StudentDetailDto>;
}