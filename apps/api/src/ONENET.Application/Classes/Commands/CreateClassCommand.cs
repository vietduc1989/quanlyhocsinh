// QUAN-20260530-2302
using MediatR;
using ONENET.Application.Classes.DTOs;
using System;

namespace ONENET.Application.Classes.Commands
{
    public record CreateClassCommand : IRequest<ClassIdDto>
    {
        public string ClassCode { get; init; } = string.Empty;
        public string ClassName { get; init; } = string.Empty;
        public string SchoolYear { get; init; } = string.Empty;
        public Guid? HomeroomTeacherId { get; init; }
    }
}