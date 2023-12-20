using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniWms.Api.Services;
using miniWms.Application.Functions.Employees.Commands.ChangePassword;
using miniWms.Application.Functions.Employees.Commands.CreateEmployee;
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
        private readonly IUserContextService _userContextService;

        public EmployeeController(
            ILogger<EmployeeController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost("create-employee")]
        public async Task<ActionResult> CreateEmployee([FromBody] CreateEmployeeCommand registerCommand)
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

        [Authorize]
        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordCommand changePasswordCommand)
        {
            if (_userContextService.GetUserId is not null)
                changePasswordCommand.EmployeeId = (Guid)_userContextService.GetUserId;
            var result = await _mediator.Send(changePasswordCommand);

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
