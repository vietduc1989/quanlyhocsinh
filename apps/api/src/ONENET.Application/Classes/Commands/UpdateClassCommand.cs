// QUAN-20260530-2302
using MediatR;
using System;

namespace ONENET.Application.Classes.Commands
{
    public record UpdateClassCommand : IRequest
    {
        public Guid Id { get; init; }
        public string ClassName { get; init; } = string.Empty;
        public string SchoolYear { get; init; } = string.Empty;
        public Guid? HomeroomTeacherId { get; init; }
        public byte[] Version { get; init; } = Array.Empty<byte>(); // For optimistic concurrency
    }
}