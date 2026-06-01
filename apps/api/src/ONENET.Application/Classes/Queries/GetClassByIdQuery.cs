// QUAN-20260530-2302
using MediatR;
using ONENET.Application.Classes.DTOs;
using System;

namespace ONENET.Application.Classes.Queries
{
    public record GetClassByIdQuery(Guid Id) : IRequest<ClassDto>;
}