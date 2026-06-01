// QUAN-20260531-154643
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ONENET.Application.Common.Models;
using ONENET.Application.Subjects.Commands;
using ONENET.Application.Subjects.DTOs;
using ONENET.Application.Subjects.Queries;
using ONENET.WebAPI.Common;

namespace ONENET.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SubjectsController> _logger;

        public SubjectsController(IMediator mediator, ILogger<SubjectsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Tạo mới một môn học.
        /// </summary>
        /// <param name="command">Dữ liệu môn học cần tạo.</param>
        /// <returns>ID của môn học đã tạo.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,ChuyenVienDaoTao")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<Guid>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
        public async Task<IActionResult> Create([FromBody] CreateSubjectCommand command)
        {
            _logger.LogInformation("API: Received CreateSubjectCommand for Code: {Code}, Name: {Name}", command.Code, command.Name);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result }, ApiResponse<Guid>.Success(result, "Môn học được tạo thành công."));
        }

        /// <summary>
        /// Lấy danh sách môn học có phân trang, tìm kiếm và sắp xếp.
        /// </summary>
        /// <param name="query">Tham số truy vấn (phân trang, tìm kiếm, sắp xếp).</param>
        /// <returns>Danh sách môn học được phân trang.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,ChuyenVienDaoTao,GiaoVien")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PaginatedList<SubjectDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
        public async Task<IActionResult> GetList([FromQuery] GetSubjectsQuery query)
        {
            _logger.LogInformation("API: Received GetSubjectsQuery with PageIndex: {PageIndex}, PageSize: {PageSize}, SearchQuery: {SearchQuery}", query.PageIndex, query.PageSize, query.SearchQuery);
            var result = await _mediator.Send(query);
            return Ok(ApiResponse<PaginatedList<SubjectDto>>.Success(result));
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một môn học theo ID.
        /// </summary>
        /// <param name="id">ID của môn học.</param>
        /// <returns>Thông tin chi tiết môn học.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,ChuyenVienDaoTao,GiaoVien")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<SubjectDetailDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("API: Received GetSubjectByIdQuery for Id: {Id}", id);
            var query = new GetSubjectByIdQuery(id);
            var result = await _mediator.Send(query);
            // NotFoundException will be handled by GlobalExceptionMiddleware or similar
            return Ok(ApiResponse<SubjectDetailDto>.Success(result));
        }

        /// <summary>
        /// Cập nhật thông tin của một môn học.
        /// </summary>
        /// <param name="id">ID của môn học cần cập nhật.</param>
        /// <param name="command">Dữ liệu cập nhật môn học.</param>
        /// <returns>Không có nội dung trả về (200 OK).</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,ChuyenVienDaoTao")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSubjectCommand command)
        {
            _logger.LogInformation("API: Received UpdateSubjectCommand for Id: {Id}", id);
            if (id != command.Id)
            {
                _logger.LogWarning("API: UpdateSubjectCommand Id mismatch. Path Id: {PathId}, Body Id: {BodyId}", id, command.Id);
                return BadRequest(ApiResponse.Failure("ID trong URL không khớp với ID trong nội dung yêu cầu."));
            }
            await _mediator.Send(command);
            return Ok(ApiResponse.Success("Môn học được cập nhật thành công."));
        }

        /// <summary>
        /// Xóa mềm một môn học (thiết lập trạng thái IsActive = false).
        /// </summary>
        /// <param name="id">ID của môn học cần xóa.</param>
        /// <returns>Không có nội dung trả về (200 OK).</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,ChuyenVienDaoTao")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            _logger.LogInformation("API: Received DeleteSubjectCommand for Id: {Id}", id);
            var command = new DeleteSubjectCommand(id);
            await _mediator.Send(command);
            return Ok(ApiResponse.Success("Môn học được xóa thành công."));
        }
    }
}