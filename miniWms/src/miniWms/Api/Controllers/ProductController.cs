using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniWms.Api.Services;
using miniWms.Application.Functions;
using miniWms.Application.Functions.Categories.Commands.CreateCategory;
using miniWms.Application.Functions.Products.Commands.CreateProduct;
using miniWms.Application.Functions.Products.Queries.GetSortedAndFilteredProducts;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;
using Sieve.Models;

namespace miniWms.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        private readonly IUserContextService _userContextService;

        public ProductController(
            ILogger<ProductController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<Product>>> GetSortedAndFilteredProducts([FromBody] SieveModel sieveModel)
        {
            return Ok(await _mediator.Send(new GetSortedAndFilteredProductsQuery(sieveModel)));
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductCommand createProductCommand)
        {
            if (_userContextService.GetUserId is not null)
                createProductCommand.CreatedBy = (Guid)_userContextService.GetUserId;

            var result = await _mediator.Send(createProductCommand);

            if (result.Success)
            {
                return Created("", result.ReturnedObj);
            }

            return BadRequest(result.ValidationErrors);
        }
    }
}
