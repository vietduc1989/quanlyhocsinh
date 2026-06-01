// QUAN-20260530-2301
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ONENET.Application.Features.Students.Commands;
using ONENET.Application.Features.Students.Queries;
using ONENET.WebAPI.Filters; // Assuming this filter is available

namespace ONENET.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<StudentsController> _logger; // Structured logging

    public StudentsController(IMediator mediator, ILogger<StudentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Lấy danh sách học sinh có hỗ trợ tìm kiếm, lọc và phân trang.
    /// </summary>
    [HttpGet]
    [AuthorizeRoles("Admin", "Teacher")] // Authorization: Admin, Teacher
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStudents([FromQuery] GetStudentsQuery query)
    {
        _logger.LogInformation("Retrieving students list with query: {@Query}", query);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy thông tin chi tiết của một học sinh theo ID.
    /// </summary>
    [HttpGet("{id}")]
    [AuthorizeRoles("Admin", "Teacher")] // Authorization: Admin, Teacher
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStudentById(Guid id)
    {
        _logger.LogInformation("Retrieving student details for Id: {StudentId}", id);
        var query = new GetStudentByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Thêm mới thông tin một học sinh vào hệ thống.
    /// </summary>
    [HttpPost]
    [AuthorizeRoles("Admin")] // Authorization: Admin only
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // For LopId/TrangThaiId not found
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand command)
    {
        _logger.LogInformation("Creating new student: {@Command}", command);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetStudentById), new { id = result.Data }, result);
    }

    /// <summary>
    /// Cập nhật thông tin của một học sinh hiện có.
    /// </summary>
    [HttpPut("{id}")]
    [AuthorizeRoles("Admin")] // Authorization: Admin only
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentCommand command)
    {
        // Ensure the ID from the route matches the command's ID
        if (id != command.Id)
        {
            _logger.LogWarning("Mismatched student ID in route ({RouteId}) and body ({BodyId}) for update operation.", id, command.Id);
            return BadRequest(Application.Common.Models.ApiResponse.ErrorResult("ID trong URL không khớp với ID trong nội dung yêu cầu."));
        }

        _logger.LogInformation("Updating student {StudentId} with command: {@Command}", id, command);
        var result = await _mediator.Send(command);
        return Ok(result); // Return 200 OK with success message
    }

    /// <summary>
    /// Xóa một học sinh khỏi hệ thống (soft delete).
    /// </summary>
    [HttpDelete("{id}")]
    [AuthorizeRoles("Admin")] // Authorization: Admin only
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)] // QUAN-20260530-2301-FR06
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        _logger.LogInformation("Deleting student with Id: {StudentId}", id);
        var command = new DeleteStudentCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}