using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions.Documents.Queries.GetDocumentById;
using miniWms.Application.Functions.WarehouseEntries.Commands.AddToStock;
using miniWms.Application.Functions.WarehouseEntries.Commands.SubtractFromStock;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Commands.DeleteDocument
{
    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, ResponseBase>
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IMediator _mediator;
        private readonly ITransactionManager _transactionManager;
        public DeleteDocumentCommandHandler(IDocumentsRepository documentsRepository, IMediator mediator, ITransactionManager transactionManager)
        {
            _documentsRepository = documentsRepository;
            _mediator = mediator;
            _transactionManager = transactionManager;
        }

        public async Task<ResponseBase> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var documentResponse = await _mediator.Send(new GetDocumentByIdWithEntriesQuery(request.DocumentId));

            if (!documentResponse.Success || documentResponse.ReturnedObj == null)
                return new ResponseBase(false, "The document does not exist.");

            var document = documentResponse.ReturnedObj;

            var operations = new Dictionary<(ActionType ActionType, bool IsComplited), Func<Task>>()
            {
                { (ActionType.InternalTransfer, IsComplited: true), async () => {
                    await _mediator.Send(new AddToStockCommand(document.MainWarehouseId, document.DocumentEntries), cancellationToken);
                    await _mediator.Send(new SubtractFromStockCommand((Guid)document.TargetWarehouseId, document.DocumentEntries), cancellationToken);
                } },

                { (ActionType.InternalTransfer, IsComplited: false), async () => {
                    await _mediator.Send(new AddToStockCommand(document.MainWarehouseId, document.DocumentEntries), cancellationToken);
                } },

                { (ActionType.InternalReceipt, IsComplited: true), async () => {
                    await _mediator.Send(new SubtractFromStockCommand(document.MainWarehouseId, document.DocumentEntries), cancellationToken);
                } },

                { (ActionType.InternalIssue, IsComplited: true), async () => {
                    await _mediator.Send(new AddToStockCommand(document.MainWarehouseId, document.DocumentEntries), cancellationToken);
                } },

                { (ActionType.ExternalReceipt, IsComplited: true), async () => {
                    await _mediator.Send(new SubtractFromStockCommand(document.MainWarehouseId, document.DocumentEntries), cancellationToken);
                } },

                { (ActionType.ExternalIssue, IsComplited: true), async () => {
                    await _mediator.Send(new AddToStockCommand(document.MainWarehouseId, document.DocumentEntries), cancellationToken);
                } }
            };

            try
            {
                await _transactionManager.BeginTransactionAsync();

                await _documentsRepository.DeleteAsync(document);

                await operations[(document.ActionType, document.IsCompleted)].Invoke();

                await _transactionManager.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new ResponseBase(false, "Something went wrong. " + ex.Message);
            }

            return new ResponseBase();
        }
    }
}
