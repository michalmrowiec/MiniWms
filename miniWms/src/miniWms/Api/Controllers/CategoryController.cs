using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniWms.Api.Services;
using miniWms.Application.Functions;
using miniWms.Application.Functions.Categories.Commands.CreateCategory;
using miniWms.Application.Functions.Categories.Queries.GetAllCategories;
using miniWms.Domain.Entities;

namespace miniWms.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;
        private readonly IUserContextService _userContextService;

        public CategoryController(
            ILogger<CategoryController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            return Ok(await _mediator.Send(new GetAllCategoriesQuery()));
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            if (_userContextService.GetUserId is not null)
                createCategoryCommand.CreatedBy = (Guid)_userContextService.GetUserId;

            var result = await _mediator.Send(createCategoryCommand);

            if (result is ResponseBase<Category> r)
            {
                return Created("", r.ReturnedObj);
            }

            return BadRequest(result.ValidationErrors);
        }
    }
}
