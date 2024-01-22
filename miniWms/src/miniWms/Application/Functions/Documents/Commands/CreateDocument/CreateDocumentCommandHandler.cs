using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Functions.DocumentEntries.Command.CreateDocumentEntries;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, ResponseBase<Document>>
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IMediator _mediator;
        public CreateDocumentCommandHandler(IDocumentsRepository documentsRepository, IMediator mediator)
        {
            _documentsRepository = documentsRepository;
            _mediator = mediator;
        }

        public async Task<ResponseBase<Document>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDocumentValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new ResponseBase<Document>(validatorResult);
            }

            var newDocument = new Document
            {
                DocumentTypeId = request.DocumentTypeId,
                SupplierId = request.SupplierId,
                RecipientId = request.RecipientId,
                Country = request.Country,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Address = request.Address,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy,
                ModifiedBy = request.CreatedBy
            };

            var createdDocument = await _documentsRepository.CreateAsync(newDocument);

            foreach (var de in request.DocumentEntries)
            {
                de.DocumentId = createdDocument.DocumentId;
            }

            var createdDocumentEntries = await _mediator.Send(
                new CreateDocumentEntriesCommand() { DocumentEntries = request.DocumentEntries, CreatedBy = request.CreatedBy });


            return new ResponseBase<Document>(createdDocument);
        }
    }
}
