using System;
using MediatR;

namespace ONENET.Application.Features.Students.Commands
{
    public record DeleteStudentCommand(Guid Id) : IRequest<ONENET.Application.Features.Students.Dtos.SuccessDto>;
}
