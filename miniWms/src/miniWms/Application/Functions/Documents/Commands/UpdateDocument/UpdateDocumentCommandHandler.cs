using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions.Documents.Queries.GetDocumentById;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, ResponseBase<Document>>
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IMediator _mediator;
        private readonly ITransactionManager _transactionManager;
        public UpdateDocumentCommandHandler(IDocumentsRepository documentsRepository, IMediator mediator, ITransactionManager transactionManager)
        {
            _documentsRepository = documentsRepository;
            _mediator = mediator;
            _transactionManager = transactionManager;
        }

        public async Task<ResponseBase<Document>> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDocumentValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new ResponseBase<Document>(validatorResult);
            }

            var documentResponse = await _mediator.Send(new GetDocumentByIdWithEntriesQuery(request.DocumentId));

            if (!documentResponse.Success || documentResponse.ReturnedObj == null)
            {
                return new ResponseBase<Document>(false, "The document does not exist.");
            }

            Document document = documentResponse.ReturnedObj;

            document.DateOfOperation = request.DateOfOperation ?? document.DateOfOperation;
            document.Comments = request.Comments ?? document.Comments;
            document.Country = request.Country ?? document.Country;
            document.City = request.City ?? document.City;
            document.Region = request.Region ?? document.Region;
            document.PostalCode = request.PostalCode ?? document.PostalCode;
            document.Address = request.Address ?? document.Address;
            document.ModifiedAt = DateTime.UtcNow;
            document.ModifiedBy = request.ModifiedBy ?? document.ModifiedBy;

            Document updatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();
                
                updatedDocument = await _documentsRepository.UpdateAsync(document);

                await _transactionManager.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new ResponseBase<Document>(false, "Something went wrong. " + ex.Message);
            }

            return new ResponseBase<Document>(updatedDocument);
        }
    }
}
