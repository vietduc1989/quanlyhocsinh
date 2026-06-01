// QUAN-20260531-154643
using MediatR;

namespace ONENET.Application.Subjects.Commands
{
    public record CreateSubjectCommand : IRequest<Guid>
    {
        public string Code { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public int Credits { get; init; }
        public bool IsActive { get; init; } = true; // Mặc định là hoạt động
    }
}