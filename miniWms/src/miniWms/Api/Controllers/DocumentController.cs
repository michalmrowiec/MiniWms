using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniWms.Api.Services;
using miniWms.Application.Functions.Documents.Documents.Commands.ApproveInternalDocument;
using miniWms.Application.Functions.Documents.Documents.Commands.CreateDocument;
using miniWms.Application.Functions.Documents.Queries.GetSortedAndFilteredDocuments;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;
using Sieve.Models;

namespace miniWms.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DocumentController> _logger;
        private readonly IUserContextService _userContextService;

        public DocumentController(
            ILogger<DocumentController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<Document>>> GetSortedAndFilteredDocuments([FromBody] SieveModel sieveModel)
        {
            return Ok(await _mediator.Send(new GetSortedAndFilteredDocumentsQuery(sieveModel)));
        }

        [HttpPost]
        public async Task<ActionResult<Document>> CreateDocument([FromBody] CreateDocumentCommand createDocument)
        {
            if (_userContextService.GetUserId is not null)
                createDocument.CreatedBy = (Guid)_userContextService.GetUserId;

            var result = await _mediator.Send(createDocument);

            if (result.Success)
            {
                return Created("", result.ReturnedObj);
            }

            return BadRequest(result);
        }

        [HttpPut("approve")]
        public async Task<ActionResult<Document>> ApproveDocument([FromBody] ApproveDocumentCommand approveDocument)
        {
            if (_userContextService.GetUserId is not null)
                approveDocument.ModifiedBy = (Guid)_userContextService.GetUserId;

            var result = await _mediator.Send(approveDocument);

            if (result.Success)
            {
                return Ok(result.ReturnedObj);
            }
            return BadRequest(result);
        }
    }
}
