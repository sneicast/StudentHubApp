using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Application.Students.Commands;
using StudentHub.Application.Students.Queries;

namespace StudentHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _mediator.Send(new GetAllStudentsQuery());
            return Ok(students);
        }



    }
}
