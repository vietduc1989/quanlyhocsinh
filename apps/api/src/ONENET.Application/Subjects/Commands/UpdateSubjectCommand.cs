// QUAN-20260531-154643
using MediatR;

namespace ONENET.Application.Subjects.Commands
{
    public record UpdateSubjectCommand : IRequest<Unit>
    {
        public Guid Id { get; init; }
        public string Code { get; init; } = default!; // Code can be updated if allowed by business, but BR states unique code.
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public int Credits { get; init; }
        public bool IsActive { get; init; }
    }
}