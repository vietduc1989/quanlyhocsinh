// QUAN-20260530-2301
using MediatR;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.Students.DTOs;

namespace ONENET.Application.Features.Students.Queries;

public record GetStudentByIdQuery(Guid Id) : IRequest<ApiResponse<StudentDetailDto>>;