using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions.WarehouseEntries.Commands.AddToStock;
using miniWms.Application.Functions.WarehouseEntries.Commands.SubtractFromStock;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.InternalDocuments.CreateInternalDocument
{
    public class CreateInternalDocumentCommandHandler : IRequestHandler<CreateInternalDocumentCommand, ResponseBase<InternalDocument>>
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public CreateInternalDocumentCommandHandler(IDocumentsRepository documentsRepository, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _documentsRepository = documentsRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<InternalDocument>> Handle(CreateInternalDocumentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateInternalDocumentValidator(_mediator);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new ResponseBase<InternalDocument>(validatorResult);
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

            var newDocument = new InternalDocument
            {
                DocumentTypeId = request.DocumentTypeId,
                WarehouseId = request.WarehouseId,
                TargetWarehouseId = request.TargetWarehouseId,
                DateOfOperation = request.DateOfOperation,
                IsComplited = request.IsComplited,
                IsStockTransfer = request.IsStockTransfer,
                IsReceived = request.IsReceived,
                DateOfOperationComplited = request.DateOfOperationComplited,
                Comments = request.Comments,
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

            Document createdInternalDocument;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                createdInternalDocument = await _documentsRepository.CreateAsync(newDocument);
                
                if(newDocument.IsStockTransfer)
                {
                    await _mediator.Send(new SubtractFromStockCommand(newDocument.WarehouseId, newDocumentEntries), cancellationToken);

                    if(newDocument.IsComplited && newDocument.TargetWarehouseId.HasValue)
                    {
                        await _mediator.Send(new AddToStockCommand((Guid)newDocument.TargetWarehouseId, newDocumentEntries), cancellationToken);
                    }
                }
                else if(!newDocument.TargetWarehouseId.HasValue && newDocument.IsComplited)
                {
                    if(newDocument.IsReceived)
                    {
                        await _mediator.Send(new AddToStockCommand(newDocument.WarehouseId, newDocumentEntries), cancellationToken);
                    }
                    else
                    {
                        await _mediator.Send(new SubtractFromStockCommand(newDocument.WarehouseId, newDocumentEntries), cancellationToken);
                    }
                }

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseBase<InternalDocument>(false, "Something went wrong." + ex.Message);
            }

            return new ResponseBase<InternalDocument>((InternalDocument)createdInternalDocument);
        }
    }
}
