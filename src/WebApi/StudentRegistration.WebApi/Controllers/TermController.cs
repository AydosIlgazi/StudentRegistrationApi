using Microsoft.AspNetCore.Mvc;
using MediatR;
using StudentRegistration.Application.Commands;

namespace StudentRegistration.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TermController : Controller
    {
        private readonly IMediator _mediator;
        public TermController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateTerm([FromBody] CreateTermCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost]
        [Route("startterm")]
        public async Task<IActionResult> StartTerm([FromBody] StartTermCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}
