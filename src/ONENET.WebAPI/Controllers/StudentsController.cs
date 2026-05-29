/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: WebAPI (Controllers)
 */

using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ONENET.Application.Students.Commands;
using ONENET.Application.Students.Queries;

namespace ONENET.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private string GetCurrentUser() => User.FindFirst(ClaimTypes.Name)?.Value ?? "lan.nguyen@onenet.vn";
        private string GetClientIpAddress() => HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

        [HttpGet]
        public async Task<IActionResult> GetStudents(
            [FromQuery] string? searchQuery, 
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 20)
        {
            var query = new GetStudentsQuery(searchQuery, page, pageSize);
            var result = await _mediator.Send(query);
            return Ok(new { success = true, data = result.Items, pagination = new { currentPage = result.CurrentPage, pageSize = result.PageSize, totalRecords = result.TotalRecords, totalPages = result.TotalPages } });
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequestDto dto)
        {
            var command = new CreateStudentCommand(
                dto.FullName,
                dto.DateOfBirth,
                dto.Gender,
                dto.Address,
                dto.ParentPhone,
                dto.Email,
                GetCurrentUser(),
                GetClientIpAddress()
            );

            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(new { success = false, message = result.Message });
            }

            return Created($"/api/v1/students/{result.Data!.Id}", new { success = true, message = result.Message, data = result.Data });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentRequestDto dto)
        {
            var command = new UpdateStudentCommand(
                id,
                dto.FullName,
                dto.DateOfBirth,
                dto.Gender,
                dto.Address,
                dto.ParentPhone,
                dto.Email,
                GetCurrentUser(),
                GetClientIpAddress()
            );

            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(new { success = false, message = result.Message });
            }

            return Ok(new { success = true, message = result.Message, data = result.Data });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var command = new DeleteStudentCommand(id, GetCurrentUser(), GetClientIpAddress());
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(new { success = false, message = result.Message });
            }

            return Ok(new { success = true, message = result.Message });
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportStudents([FromQuery] string? searchQuery)
        {
            var query = new ExportStudentsQuery(searchQuery, GetCurrentUser(), GetClientIpAddress());
            var result = await _mediator.Send(query);
            return File(result.FileContents, result.ContentType, result.FileName);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportStudents(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { success = false, message = "File upload rỗng." });
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var command = new ImportStudentsCommand(memoryStream, GetCurrentUser(), GetClientIpAddress());
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                if (result.ErrorReportBytes != null)
                {
                    // Trả ra file báo lỗi trực tiếp
                    return File(result.ErrorReportBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Error_Import_Report.xlsx");
                }
                return BadRequest(new { success = false, message = result.Message });
            }

            return Ok(new { success = true, message = result.Message });
        }
    }

    public class CreateStudentRequestDto
    {
        public string FullName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string? Address { get; set; }
        public string ParentPhone { get; set; } = null!;
        public string? Email { get; set; }
    }

    public class UpdateStudentRequestDto
    {
        public string FullName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string? Address { get; set; }
        public string ParentPhone { get; set; } = null!;
        public string? Email { get; set; }
    }
}