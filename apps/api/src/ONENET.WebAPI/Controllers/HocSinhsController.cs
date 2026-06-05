// QUAN-20260604-153038
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ONENET.Application.Features.HocSinhs.Commands;
using ONENET.Application.Features.HocSinhs.DTOs;
using ONENET.Application.Features.HocSinhs.Queries;
using ONENET.Application.Features.LopHocs.DTOs;
using ONENET.Application.Features.LopHocs.Queries;
using ONENET.WebAPI.Filters;
using ONENET.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONENET.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // NFR: Security - Only Admin has CRUD rights
    public class HocSinhsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HocSinhsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // FR01, FR07, FR08: Get all students with search, filter, pagination
        [HttpGet]
        public async Task<IActionResult> GetHocSinhs([FromQuery] GetHocSinhsQuery query)
        {
            var result = await _mediator.Send(query);
            return result.IsSuccess
                ? Ok(ApiResponse<object>.Success(result.Value, "Danh sách học sinh được tải thành công."))
                : BadRequest(ApiResponse<object>.Failure(result.Errors, "Tải danh sách học sinh thất bại."));
        }

        // FR02: Get student by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHocSinhById(Guid id)
        {
            var result = await _mediator.Send(new GetHocSinhByIdQuery { Id = id });
            return result.IsSuccess
                ? Ok(ApiResponse<HocSinhDto>.Success(result.Value, "Chi tiết học sinh được tải thành công."))
                : NotFound(ApiResponse<HocSinhDto>.Failure(result.Errors, "Không tìm thấy học sinh."));
        }

        // FR03: Create new student
        [HttpPost]
        public async Task<IActionResult> CreateHocSinh([FromBody] CreateHocSinhCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? CreatedAtAction(nameof(GetHocSinhById), new { id = result.Value?.Id }, ApiResponse<HocSinhDto>.Success(result.Value, "Thêm học sinh thành công."))
                : BadRequest(ApiResponse<HocSinhDto>.Failure(result.Errors, "Thêm học sinh thất bại."));
        }

        // FR04: Update existing student
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHocSinh(Guid id, [FromBody] UpdateHocSinhCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(ApiResponse<HocSinhDto>.Failure("ID không khớp."));
            }
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? Ok(ApiResponse<HocSinhDto>.Success(result.Value, "Cập nhật học sinh thành công."))
                : BadRequest(ApiResponse<HocSinhDto>.Failure(result.Errors, "Cập nhật học sinh thất bại."));
        }

        // FR05, FR06: Soft delete student
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHocSinh(Guid id)
        {
            var result = await _mediator.Send(new DeleteHocSinhCommand { Id = id });
            return result.IsSuccess
                ? Ok(ApiResponse<object>.Success(null, "Xóa học sinh thành công."))
                : BadRequest(ApiResponse<object>.Failure(result.Errors, "Xóa học sinh thất bại."));
        }

        // FR07: Get LopHocs for filter dropdown
        [HttpGet("lop-hocs-lookup")]
        public async Task<IActionResult> GetLopHocsForLookup([FromQuery] GetLopHocsForFilterQuery query)
        {
            var result = await _mediator.Send(query);
            return result.IsSuccess
                ? Ok(ApiResponse<IReadOnlyList<LopHocLookupDto>>.Success(result.Value, "Danh sách lớp học được tải thành công."))
                : BadRequest(ApiResponse<object>.Failure(result.Errors, "Tải danh sách lớp học thất bại."));
        }
    }
}