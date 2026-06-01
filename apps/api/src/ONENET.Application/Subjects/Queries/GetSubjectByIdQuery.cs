// QUAN-20260531-154643
using MediatR;
using ONENET.Application.Subjects.DTOs;

namespace ONENET.Application.Subjects.Queries
{
    public record GetSubjectByIdQuery(Guid Id) : IRequest<SubjectDetailDto?>;
}