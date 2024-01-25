using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniWms.Api.Services;
using miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType;
using miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes;
using miniWms.Domain.Entities;

namespace miniWms.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DocumentTypeController> _logger;
        private readonly IUserContextService _userContextService;

        public DocumentTypeController(
            ILogger<DocumentTypeController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<DocumentType>>> GetAllDocumentTypes()
        {
            return Ok(await _mediator.Send(new GetAllDocumentTypesQuery()));
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<DocumentType>> CreateDocumentType([FromBody] CreateDocumentTypeCommand createCategoryCommand)
        {
            if (_userContextService.GetUserId is not null)
                createCategoryCommand.CreatedBy = (Guid)_userContextService.GetUserId;

            var result = await _mediator.Send(createCategoryCommand);

            if (result.Success)
            {
                return Created("", result.ReturnedObj);
            }

            return BadRequest(result.ValidationErrors);
        }
    }
}
