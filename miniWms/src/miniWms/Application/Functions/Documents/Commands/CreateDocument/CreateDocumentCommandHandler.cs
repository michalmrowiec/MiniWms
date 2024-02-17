using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions.WarehouseEntries.Commands.AddToStock;
using miniWms.Application.Functions.WarehouseEntries.Commands.SubtractFromStock;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, ResponseBase<Document>>
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IMediator _mediator;
        private readonly IUnitOfTransaction _unitOfWork;
        public CreateDocumentCommandHandler(IDocumentsRepository documentsRepository, IMediator mediator, IUnitOfTransaction unitOfWork)
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

            var newDocument = new Document
            {
                DocumentTypeId = request.DocumentTypeId,
                MainWarehouseId = request.MainWarehouseId,
                TargetWarehouseId = request.TargetWarehouseId,
                ContractorId = request.ContractorId,
                ActionType = request.ActionType,
                IsCompleted = request.IsCompleted,
                DateOfOperation = request.DateOfOperation,
                DateOfOperationCompleted = request.DateOfOperationCompleted,
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

            if (newDocument.DateOfOperationCompleted == null)
                newDocument.DateOfOperationCompleted = newDocument.DateOfOperation;

            Document createdDocument;


            var operations = new Dictionary<(ActionType ActionType, bool IsComplited), Func<Task>>()
            {
                { (ActionType.InternalTransfer, IsComplited: true), async () => {
                    await _mediator.Send(new SubtractFromStockCommand(newDocument.MainWarehouseId, newDocumentEntries), cancellationToken);
                    await _mediator.Send(new AddToStockCommand((Guid)newDocument.TargetWarehouseId, newDocumentEntries), cancellationToken);
                } },

                { (ActionType.InternalTransfer, IsComplited: false), async () => {
                    await _mediator.Send(new SubtractFromStockCommand(newDocument.MainWarehouseId, newDocumentEntries), cancellationToken);
                } },

                { (ActionType.InternalReceipt, IsComplited: true), async () => {
                    await _mediator.Send(new AddToStockCommand(newDocument.MainWarehouseId, newDocumentEntries), cancellationToken);
                } },

                { (ActionType.InternalIssue, IsComplited: true), async () => {
                    await _mediator.Send(new SubtractFromStockCommand(newDocument.MainWarehouseId, newDocumentEntries), cancellationToken);
                } },

                { (ActionType.ExternalReceipt, IsComplited: true), async () => {
                    await _mediator.Send(new AddToStockCommand(newDocument.MainWarehouseId, newDocumentEntries), cancellationToken);
                } },

                { (ActionType.ExternalIssue, IsComplited: true), async () => {
                    await _mediator.Send(new SubtractFromStockCommand(newDocument.MainWarehouseId, newDocumentEntries), cancellationToken);
                } }
            };

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                createdDocument = await _documentsRepository.CreateAsync(newDocument);

                await operations[(newDocument.ActionType, newDocument.IsCompleted)].Invoke();

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseBase<Document>(false, "Something went wrong." + ex.Message);
            }

            return new ResponseBase<Document>(createdDocument);
        }
    }
}
