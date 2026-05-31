// QUAN-20260530-2301
using MediatR;
using ONENET.Application.Features.Students.Dtos;
using System;

namespace ONENET.Application.Features.Students.Commands
{
    public record CreateStudentCommand(CreateStudentDto Student) : IRequest<StudentIdDto>;
}