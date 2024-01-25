﻿using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions.Documents.Queries.GetDocumentById;
using miniWms.Application.Functions.WarehouseEntries.Commands.AddToStock;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Documents.Commands.ApproveInternalDocument
{
    public class ApproveInternalDocumentCommandHandler : IRequestHandler<ApproveInternalDocumentCommand, ResponseBase<Document>>
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public ApproveInternalDocumentCommandHandler(IDocumentsRepository documentsRepository, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _documentsRepository = documentsRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<Document>> Handle(ApproveInternalDocumentCommand request, CancellationToken cancellationToken)
        {
            var validator = new ApproveInternalDocumentValidator(_mediator);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new ResponseBase<Document>(validatorResult);
            }

            var documentResponse = await _mediator.Send(new GetDocumentByIdQuery(request.DocumentId), cancellationToken);


            if (documentResponse.ReturnedObj is not Document internalDocument)
            {
                return new ResponseBase<Document>(false, "Something went wrong.");
            }

            internalDocument.IsComplited = true;
            internalDocument.DateOfOperationComplited = request.DateOfOperationComplited;
            internalDocument.ModifiedBy = request.ModifiedBy;
            internalDocument.ModifiedAt = DateTime.UtcNow;

            Document updatedInternalDocument;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                updatedInternalDocument = await _documentsRepository.UpdateAsync(internalDocument);

                if (internalDocument.TargetWarehouseId.HasValue)
                {
                    await _mediator.Send(new AddToStockCommand((Guid)internalDocument.TargetWarehouseId, internalDocument.DocumentEntries), cancellationToken);
                }

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseBase<Document>(false, "Something went wrong." + e.Message);
            }

            return new ResponseBase<Document>((Document)updatedInternalDocument);
        }
    }
}