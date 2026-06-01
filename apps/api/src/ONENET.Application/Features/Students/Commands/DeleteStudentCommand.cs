<!-- QUAN-20260530-2301 -->
using System;
using MediatR;

namespace ONENET.Application.Features.Students.Commands
{
    public record DeleteStudentCommand(Guid Id) : IRequest<Unit>;
}