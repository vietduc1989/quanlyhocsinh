using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ONENET.Application.Students.Commands.CreateStudent;
using ONENET.Application.Students.Commands.DeleteStudent;
using ONENET.Application.Students.Commands.UpdateStudent;
using ONENET.Application.Students.Dtos;
using ONENET.Application.Students.Queries.GetStudentById;
using ONENET.Application.Students.Queries.GetStudents;

namespace ONENET.WebAPI.Controllers;

/// <summary>
/// Controller xử lý các HTTP Requests cho module quản lý học sinh.
/// </summary>
public class StudentsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedListDto<StudentDto>>> Get([FromQuery] GetStudentsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StudentDto>> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetStudentByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { Message = "Học sinh không tồn tại." });
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateStudentCommand command)
    {
        try
        {
            var id = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateStudentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new { Message = "Mã ID không khớp." });
        }

        try
        {
            await Mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Message = "Học sinh không tồn tại." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await Mediator.Send(new DeleteStudentCommand(id));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Message = "Học sinh không tồn tại." });
        }
    }
}