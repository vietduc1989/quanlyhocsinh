// QUAN-20260530-2301
using MediatR;
using ONENET.Application.Features.Students.Dtos;
using System;

namespace ONENET.Application.Features.Students.Commands
{
    public record UpdateStudentCommand(Guid Id, UpdateStudentDto Student) : IRequest<SuccessDto>;
}