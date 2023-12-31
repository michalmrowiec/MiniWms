using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniWms.Api.Services;
using miniWms.Application.Functions.Warehouses.Commands.CreateWarehouse;
using miniWms.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using miniWms.Domain.Entities;

namespace miniWms.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WarehouseController> _logger;
        private readonly IUserContextService _userContextService;

        public WarehouseController(
            ILogger<WarehouseController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Warehouse>>> GetAllWarehouses()
        {
            return Ok(await _mediator.Send(new GetAllWarehousesQuery()));
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<Warehouse>> CreateWarehouse(CreateWarehouseCommand createWarehouseCommand)
        {
            if (_userContextService.GetUserId is not null)
                createWarehouseCommand.CreatedBy = (Guid)_userContextService.GetUserId;

            var result = await _mediator.Send(createWarehouseCommand);

            if (result.Success)
                return Created("", result.Warehouse);

            return BadRequest(result.ValidationErrors);
        }
    }
}
