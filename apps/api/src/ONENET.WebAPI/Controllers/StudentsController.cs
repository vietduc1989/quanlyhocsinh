<!-- QUAN-20260530-2301 -->
using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ONENET.Application.Features.Students.Commands;
using ONENET.Application.Features.Students.DTOs;
using ONENET.Application.Features.Students.Queries;
using ONENET.WebAPI.Common;

namespace ONENET.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // QUAN-20260530-2301-SRS01, QUAN-20260530-2301-SRS07, QUAN-20260530-2301-SRS08
        // Lấy danh sách Học sinh (View List, Search, Filter, Pagination)
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        [ProducesResponseType(typeof(ApiResponse<PagedListDto<StudentDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetStudents([FromQuery] GetStudentsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(ApiResponse<PagedListDto<StudentDto>>.SuccessResponse(result, "Danh sách học sinh"));
        }

        // QUAN-20260530-2301-SRS02
        // Lấy chi tiết Học sinh (View Details)
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        [ProducesResponseType(typeof(ApiResponse<StudentDetailDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var query = new GetStudentByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(ApiResponse<StudentDetailDto>.SuccessResponse(result, "Chi tiết học sinh"));
        }

        // QUAN-20260530-2301-SRS03
        // Thêm mới Học sinh (Create)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<object>), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand command)
        {
            var studentId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetStudentById), new { id = studentId }, ApiResponse.SuccessResponse(new { id = studentId }, "Thêm học sinh thành công"));
        }

        // QUAN-20260530-2301-SRS04
        // Cập nhật thông tin Học sinh (Update)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(ApiResponse.ErrorResponse("ID trong URL và ID trong request body không khớp."));
            }
            await _mediator.Send(command);
            return Ok(ApiResponse.SuccessResponse(null, "Cập nhật học sinh thành công"));
        }

        // QUAN-20260530-2301-SRS05, QUAN-20260530-2301-SRS06
        // Xóa Học sinh (Delete)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 409)] // 409 Conflict for related data
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var command = new DeleteStudentCommand(id);
            await _mediator.Send(command);
            return Ok(ApiResponse.SuccessResponse(null, "Xóa học sinh thành công"));
        }
    }
}