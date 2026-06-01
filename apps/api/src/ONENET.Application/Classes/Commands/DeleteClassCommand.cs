// QUAN-20260530-2302
using MediatR;
using System;

namespace ONENET.Application.Classes.Commands
{
    public record DeleteClassCommand(Guid Id) : IRequest;
}