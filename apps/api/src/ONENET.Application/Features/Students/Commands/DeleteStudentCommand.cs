// QUAN-20260530-2301
using MediatR;
using ONENET.Application.Common.Models;

namespace ONENET.Application.Features.Students.Commands;

public record DeleteStudentCommand(Guid Id) : IRequest<ApiResponse>;