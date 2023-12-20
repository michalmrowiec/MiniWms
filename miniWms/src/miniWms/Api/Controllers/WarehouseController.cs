using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniWms.Api.Services;

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
    }
}
