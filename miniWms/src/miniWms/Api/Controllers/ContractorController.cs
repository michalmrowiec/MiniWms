using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniWms.Api.Services;
using miniWms.Application.Functions;
using miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType;
using miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes;
using miniWms.Domain.Entities;

namespace miniWms.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContractorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ContractorController> _logger;
        private readonly IUserContextService _userContextService;

        public ContractorController(
            ILogger<ContractorController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Contractor>>> GetAllContractors()
        {
            return Ok(await _mediator.Send(new GetAllContractorsQuery()));
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<Contractor>> CreateContractor([FromBody] CreateContractorCommand createContractor)
        {
            if (_userContextService.GetUserId is not null)
                createContractor.CreatedBy = (Guid)_userContextService.GetUserId;

            var result = await _mediator.Send(createContractor);

            if (result.Success)
            {
                return Created("", result.ReturnedObj);
            }

            return BadRequest(result);
        }
    }
}
