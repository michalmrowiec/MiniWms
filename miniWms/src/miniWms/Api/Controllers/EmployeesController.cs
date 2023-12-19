using MediatR;
using Microsoft.AspNetCore.Mvc;
using miniWms.Application.Functions.Employees;
using miniWms.Application.Functions.Employees.Commands;

namespace miniWms.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("add-employee")]
        public async Task<ActionResult<JwtToken>> AddEmployee([FromBody] AddEmployeeCommand registerCommand)
        {
            var result = await _mediator.Send(registerCommand);

            if (result.Success)
                return Ok(result.JwtToken);

            return BadRequest(result.ValidationErrors);
        }
    }
}
