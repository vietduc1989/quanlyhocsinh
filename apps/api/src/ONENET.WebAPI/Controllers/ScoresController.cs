// QUAN-20260531-154643
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ONENET.Application.Features.Scores.Commands;
using ONENET.Application.Features.Scores.DTOs;
using ONENET.Application.Features.Scores.Queries;
using ONENET.WebAPI.Common; // Assuming ApiResponse is in Common

namespace ONENET.WebAPI.Controllers
{
    [Authorize] // All endpoints require authentication
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ScoresController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ScoresController> _logger; // Use ILogger from Serilog

        public ScoresController(IMediator mediator, ILogger<ScoresController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new score record for a student in a subject and semester.
        /// </summary>
        /// <param name="command">The score creation command.</param>
        /// <returns>A 201 Created response with the ID of the new score.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")] // BR04: Admin and Teacher can create
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<Guid>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiResponse))]
        public async Task<IActionResult> Create([FromBody] CreateScoreCommand command)
        {
            _logger.LogInformation("Received CreateScoreCommand for StudentId: {StudentId}, SubjectId: {SubjectId}, SemesterId: {SemesterId}",
                                   command.StudentId, command.SubjectId, command.SemesterId);
            var scoreId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = scoreId }, ApiResponse<Guid>.Success(scoreId, "Score created successfully."));
        }

        /// <summary>
        /// Retrieves a paginated list of scores, with optional filtering, searching, and sorting.
        /// </summary>
        /// <param name="query">The query parameters for filtering, paging, and sorting.</param>
        /// <returns>A 200 OK response with a paginated list of scores.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")] // BR04: Admin and Teacher can view list
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PaginatedList<ScoreListItemDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
        public async Task<IActionResult> GetList([FromQuery] GetScoresQuery query)
        {
            _logger.LogInformation("Received GetScoresQuery: Page {PageNumber}, Size {PageSize}, Search: {SearchKeyword}",
                                   query.PageNumber, query.PageSize, query.SearchKeyword);
            var result = await _mediator.Send(query);
            return Ok(ApiResponse<PaginatedList<ScoreListItemDto>>.Success(result, "Scores retrieved successfully."));
        }

        /// <summary>
        /// Retrieves the detailed information for a specific score record.
        /// </summary>
        /// <param name="id">The ID of the score to retrieve.</param>
        /// <returns>A 200 OK response with the score details.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher")] // BR04: Admin and Teacher can view detail
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ScoreDto>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Received GetScoreByIdQuery for ScoreId: {ScoreId}", id);
            var result = await _mediator.Send(new GetScoreByIdQuery(id));
            return Ok(ApiResponse<ScoreDto>.Success(result, "Score detail retrieved successfully."));
        }

        /// <summary>
        /// Updates the value of an existing score record.
        /// </summary>
        /// <param name="id">The ID of the score to update.</param>
        /// <param name="command">The update command containing the new score value.</param>
        /// <returns>A 200 OK response indicating success.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Teacher")] // BR04: Admin and Teacher can update
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateScoreCommand command)
        {
            // Ensure ID from URL matches ID in command
            if (id != command.Id)
            {
                _logger.Warning("Mismatch between route ID {RouteId} and command ID {CommandId} for UpdateScoreCommand.", id, command.Id);
                return BadRequest(ApiResponse.Error("ID in URL does not match ID in request body.", StatusCodes.Status400BadRequest));
            }

            _logger.LogInformation("Received UpdateScoreCommand for ScoreId: {ScoreId} with new Value: {NewValue}", id, command.Value);
            await _mediator.Send(command);
            return Ok(ApiResponse.Success("Score updated successfully."));
        }

        /// <summary>
        /// Deletes an existing score record (soft delete).
        /// </summary>
        /// <param name="id">The ID of the score to delete.</param>
        /// <returns>A 200 OK response indicating success.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Teacher")] // BR04: Admin and Teacher can delete
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Received DeleteScoreCommand for ScoreId: {ScoreId}", id);
            await _mediator.Send(new DeleteScoreCommand(id));
            return Ok(ApiResponse.Success("Score deleted successfully."));
        }
    }
}