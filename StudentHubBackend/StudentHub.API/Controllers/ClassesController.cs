using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAvailableClasses(CancellationToken cancellationToken)
        {
            var classes = await _mediator.Send(new StudentHub.Application.Classes.Queries.GetAvailableClassesQuery(), cancellationToken);
            return Ok(classes);
        }
    }
}
