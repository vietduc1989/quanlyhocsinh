// QUAN-20260530-2302
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ONENET.Application.Classes.Commands;
using ONENET.Application.Classes.Queries;
using ONENET.Application.Common.Models;
using ONENET.WebAPI.Models;
using System;
using System.Threading.Tasks;

namespace ONENET.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy danh sách lớp học với các tùy chọn tìm kiếm và phân trang.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher,Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        public async Task<IActionResult> GetClasses([FromQuery] GetClassesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(ApiResponse.Success(result));
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một lớp học theo ID.
        /// </summary>
        /// <param name="id">ID của lớp học</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher,Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        public async Task<IActionResult> GetClassById(Guid id)
        {
            var result = await _mediator.Send(new GetClassByIdQuery(id));
            return Ok(ApiResponse.Success(result));
        }

        /// <summary>
        /// Tạo một lớp học mới trong hệ thống.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetClassById), new { id = result.Id }, ApiResponse.Success(result));
        }

        /// <summary>
        /// Cập nhật thông tin của một lớp học hiện có.
        /// </summary>
        /// <param name="id">ID của lớp học cần cập nhật</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
        public async Task<IActionResult> UpdateClass(Guid id, [FromBody] UpdateClassCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(ApiResponse.Failure("ID trong URL và ID trong request body không khớp."));
            }

            await _mediator.Send(command);
            return Ok(ApiResponse.Success(message: "Cập nhật lớp học thành công."));
        }

        /// <summary>
        /// Xóa một lớp học khỏi hệ thống sau khi kiểm tra các ràng buộc dữ liệu.
        /// </summary>
        /// <param name="id">ID của lớp học cần xóa</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
        public async Task<IActionResult> DeleteClass(Guid id)
        {
            await _mediator.Send(new DeleteClassCommand(id));
            return Ok(ApiResponse.Success(message: "Xóa lớp học thành công."));
        }
    }
}