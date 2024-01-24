using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType
{
    public class CreateDocumentTypeCommandHandler : IRequestHandler<CreateDocumentTypeCommand, ResponseBase<DocumentType>>
    {
        private readonly IDocumentTypesRepository _documentTypesRepository;
        private readonly IMediator _mediator;

        public CreateDocumentTypeCommandHandler(IDocumentTypesRepository documentTypesRepository, IMediator mediator)
        {
            _documentTypesRepository = documentTypesRepository;
            _mediator = mediator;
        }

        public async Task<ResponseBase<DocumentType>> Handle(CreateDocumentTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDocumentTypeValidator(_mediator);
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new ResponseBase<DocumentType>(validatorResult);
            }

            var newDocumentType = new DocumentType
            {
                DocumentTypeId = request.DocumentTypeId,
                DocumentTypeName = request.DocumentTypeName,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy,
                ModifiedBy = request.CreatedBy
            };

            var createdDocumentType = await _documentTypesRepository.CreateAsync(newDocumentType);

            return new ResponseBase<DocumentType>(createdDocumentType);
        }
    }
}
