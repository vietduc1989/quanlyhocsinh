// QUAN-20260530-2301
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Features.Students.Commands;
using ONENET.Application.Features.Students.Dtos;
using ONENET.Application.Features.Students.Queries;
using ONENET.WebAPI.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IMediator mediator, ILogger<StudentsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // QUAN-20260530-2301-SRS01, SRS07, SRS08
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetStudentsPaged([FromQuery] GetStudentsPagedQuery query, CancellationToken ct)
        {
            _logger.LogInformation("Retrieving paged students list with parameters: {@Query}", query);
            var result = await _mediator.Send(query, ct);
            return Ok(ApiResponse.Success(result, "Danh sách học sinh"));
        }

        // QUAN-20260530-2301-SRS02
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetStudentById(Guid id, CancellationToken ct)
        {
            _logger.LogInformation("Retrieving student details for ID: {StudentId}", id);
            var query = new GetStudentByIdQuery(id);
            var result = await _mediator.Send(query, ct);
            return Ok(ApiResponse.Success(result, "Chi tiết học sinh"));
        }

        // QUAN-20260530-2301-SRS03
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto studentDto, CancellationToken ct)
        {
            _logger.LogInformation("Creating new student: {@StudentDto}", studentDto);
            var command = new CreateStudentCommand(studentDto);
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetStudentById), new { id = result.Id }, ApiResponse.Success(result, "Thêm học sinh thành công"));
        }

        // QUAN-20260530-2301-SRS04
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentDto studentDto, CancellationToken ct)
        {
            _logger.LogInformation("Updating student ID: {StudentId} with data: {@StudentDto}", id, studentDto);
            var command = new UpdateStudentCommand(id, studentDto);
            var result = await _mediator.Send(command, ct);
            return Ok(ApiResponse.Success(result.Success, "Cập nhật học sinh thành công"));
        }

        // QUAN-20260530-2301-SRS05, SRS06
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(Guid id, CancellationToken ct)
        {
            _logger.LogInformation("Attempting to delete student ID: {StudentId}", id);
            var command = new DeleteStudentCommand(id);
            var result = await _mediator.Send(command, ct);
            return Ok(ApiResponse.Success(result.Success, "Xóa học sinh thành công"));
        }
    }
}