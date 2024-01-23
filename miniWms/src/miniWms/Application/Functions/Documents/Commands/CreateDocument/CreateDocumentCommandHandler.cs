using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions.WarehouseEntries.Commands.UpdateStock;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, ResponseBase<Document>>
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public CreateDocumentCommandHandler(IDocumentsRepository documentsRepository, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _documentsRepository = documentsRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<Document>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDocumentValidator(_mediator);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new ResponseBase<Document>(validatorResult);
            }

            var newDecumentEntries = new List<DocumentEntry>();
            foreach (var documentEntry in request.DocumentEntries)
            {
                newDecumentEntries.Add(new DocumentEntry
                {
                    ProductId = documentEntry.ProductId,
                    Quantity = documentEntry.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    CreatedBy = request.CreatedBy,
                    ModifiedBy = request.CreatedBy
                });
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
                DocumentEntries = newDecumentEntries,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy,
                ModifiedBy = request.CreatedBy
            };

            Document createdDocument;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                createdDocument = await _documentsRepository.CreateAsync(newDocument);

                var res = await _mediator.Send(new UpdateStockCommand(newDocument), cancellationToken);

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseBase<Document>(false, "Something went wrong." + e.Message);
            }

            return new ResponseBase<Document>(createdDocument);
        }
    }
}
