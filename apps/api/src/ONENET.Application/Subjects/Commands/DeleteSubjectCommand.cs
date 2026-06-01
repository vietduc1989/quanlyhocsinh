// QUAN-20260531-154643
using MediatR;

namespace ONENET.Application.Subjects.Commands
{
    public record DeleteSubjectCommand(Guid Id) : IRequest<Unit>;
}