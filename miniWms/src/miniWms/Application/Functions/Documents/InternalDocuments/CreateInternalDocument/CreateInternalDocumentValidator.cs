using FluentValidation;
using MediatR;

namespace miniWms.Application.Functions.Documents.InternalDocuments.CreateInternalDocument
{
    public class CreateInternalDocumentValidator : CreateDocumentValidator<CreateInternalDocumentCommand>
    {
        public CreateInternalDocumentValidator(IMediator mediator) : base(mediator)
        {
            RuleFor(id => id.IsComplited)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Equal(true).When(id => !id.IsStockTransfer)
                .WithMessage("IsComplited must be true when IsStockTransfer is false"); ;

            RuleFor(id => id.IsStockTransfer)
                .Equal(true).When(id => id.TargetWarehouseId.HasValue)
                .WithMessage("IsStockTransfer must be true when TargetWarehouseId has a value");

            RuleFor(id => id.IsReceived)
                .Equal(false).When(id => id.IsStockTransfer)
                .WithMessage("IsReceived must be false when IsStockTransfer is true");

            RuleFor(id => id.TargetWarehouseId);// warehosue exist checkS
            /*                .Custom((value, context) =>
            {
                var roles = _mediator.Send(new GetAllDocumentTypesQuery()).Result;
                if (!roles.Select(r => r.DocumentTypeId).Contains(value))
                    context.AddFailure("DocumentTypeId", "Document type doesn't exist");
            });*/
        }
    }
}
