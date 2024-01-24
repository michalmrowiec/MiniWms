using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions.WarehouseEntries.Commands.AddToStock;
using miniWms.Application.Functions.WarehouseEntries.Commands.SubtractFromStock;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.ExternalDocuments.Commands
{
    public class CreateExternalDocumentCommandHandler : IRequestHandler<CreateExternalDocumentCommand, ResponseBase<ExternalDocument>>
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public CreateExternalDocumentCommandHandler(IDocumentsRepository documentsRepository, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _documentsRepository = documentsRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<ExternalDocument>> Handle(CreateExternalDocumentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExternalDocumentValidator(_mediator);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new ResponseBase<ExternalDocument>(validatorResult);
            }

            var newDocumentEntries = new List<DocumentEntry>();
            foreach (var documentEntry in request.DocumentEntries)
            {
                newDocumentEntries.Add(new DocumentEntry
                {
                    ProductId = documentEntry.ProductId,
                    Quantity = documentEntry.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    CreatedBy = request.CreatedBy,
                    ModifiedBy = request.CreatedBy
                });
            }

            var newDocument = new ExternalDocument
            {
                DocumentTypeId = request.DocumentTypeId,
                WarehouseId = request.WarehouseId,
                ContractorId = request.ContractorId,
                ContractorIsSupplier = request.ContractorIsSupplier,
                Comments = request.Comments,
                DateOfOperation = request.DateOfOperation,
                Country = request.Country,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Address = request.Address,
                DocumentEntries = newDocumentEntries,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy,
                ModifiedBy = request.CreatedBy
            };

            Document createdExternalDocument;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                createdExternalDocument = await _documentsRepository.CreateAsync(newDocument);

                if (newDocument.ContractorIsSupplier)
                    await _mediator.Send(new AddToStockCommand(newDocument.WarehouseId, newDocumentEntries), cancellationToken);
                else
                    await _mediator.Send(new SubtractFromStockCommand(newDocument.WarehouseId, newDocumentEntries), cancellationToken);

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseBase<ExternalDocument>(false, "Something went wrong." + ex.Message);
            }

            return new ResponseBase<ExternalDocument>((ExternalDocument)createdExternalDocument);
        }
    }
}
