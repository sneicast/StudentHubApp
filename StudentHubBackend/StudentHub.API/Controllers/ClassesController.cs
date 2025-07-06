using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Application.Classes.Commands;

namespace StudentHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("available")]
        [Authorize]
        public async Task<IActionResult> GetAvailableClasses(CancellationToken cancellationToken)
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid user ID.");
            }

            var query = new StudentHub.Application.Classes.Queries.GetAvailableClassesQuery
            {
                StudentId = userId
            };

            var classes = await _mediator.Send(query, cancellationToken);
            return Ok(classes);
        }
        [HttpPost("enroll")]
        [Authorize]
        public async Task<IActionResult> EnrollInClass([FromBody] ClassEnrollmentCommand command, CancellationToken cancellationToken)
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid user ID.");
            }
            command.StudentId = userId;
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        [HttpGet("enrollments")]
        [Authorize]
        public async Task<IActionResult> GetEnrollmentsByStudentId(CancellationToken cancellationToken)
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid user ID.");
            }
            var query = new StudentHub.Application.Classes.Queries.GetEnrollmentsByStudentIdQuery
            {
                StudentId = userId
            };
            var enrollments = await _mediator.Send(query, cancellationToken);
            return Ok(enrollments);
        }
    }
}
