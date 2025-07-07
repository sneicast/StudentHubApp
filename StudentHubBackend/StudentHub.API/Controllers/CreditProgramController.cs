using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Application.CreditPrograms.Queries;

namespace StudentHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditProgramController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CreditProgramController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var creditPrograms = await _mediator.Send(new GetAllCreditProgramsQuery());
            return Ok(creditPrograms);

        }
    }
}
