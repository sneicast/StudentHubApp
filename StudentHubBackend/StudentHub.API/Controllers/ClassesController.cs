using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Application.Classes.Commands;
using StudentHub.Application.Classes.Queries;

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
            var query = new GetAvailableClassesQuery
            {
                StudentId = GetUserId()
            };

            var classes = await _mediator.Send(query, cancellationToken);
            return Ok(classes);
        }

        [HttpPost("student/enroll")]
        [Authorize]
        public async Task<IActionResult> EnrollInClass([FromBody] ClassEnrollmentCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = GetUserId();
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        [HttpGet("student")]
        [Authorize]
        public async Task<IActionResult> GetEnrollmentsByStudentId(CancellationToken cancellationToken)
        {

            var query = new GetEnrollmentsByStudentIdQuery
            {
                StudentId = GetUserId()
            };
            var enrollments = await _mediator.Send(query, cancellationToken);
            return Ok(enrollments);
        }
        //unenroll student from class

        [HttpDelete("{classId}/unenroll")]
        [Authorize]
        public async Task<IActionResult> RemoveStudentClass(int classId, CancellationToken cancellationToken)
        {
            var command = new RemoveStudentClassCommand
            {
                StudentId = GetUserId(),
                ClassId = classId
            };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        //Obtener lista de estudiantes inscritos en una clase
        [HttpGet("{classId}/students")]
        [Authorize]
        public async Task<IActionResult> GetStudentsInClass(int classId, CancellationToken cancellationToken)
        {
            var query = new GetStudentsInClassQuery
            {
                ClassId = classId,
                StudentId = GetUserId()
            };
            var students = await _mediator.Send(query, cancellationToken);
            return Ok(students);
        }


        private int GetUserId() { 
            
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out var userId))
            {
                throw new InvalidOperationException("El userId es invalido");
            }
            return userId;
        }
    }
}
