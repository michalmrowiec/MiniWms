using MediatR;
using Microsoft.AspNetCore.Mvc;
using miniWms.Application.Functions.Employees.Commands.AddEmployee;
using miniWms.Application.Functions.Employees.Commands.Login;
using miniWms.Domain.Models;

namespace miniWms.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("add-employee")]
        public async Task<ActionResult> AddEmployee([FromBody] AddEmployeeCommand registerCommand)
        {
            var result = await _mediator.Send(registerCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ValidationErrors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<JwtToken>> Login([FromBody] LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand);

            if (result.Success)
                return Ok(result.JwtToken);

            return BadRequest(result.Message);
        }
    }
}
