using FluentValidation;
using MediatR;
using miniWms.Application.Functions.Documents.Queries.GetDocumentById;

namespace miniWms.Application.Functions.Documents.Commands.ApproveInternalDocument
{
    public class ApproveDocumentValidator : AbstractValidator<ApproveDocumentCommand>
    {
        private readonly IMediator _mediator;
        public ApproveDocumentValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(ad => ad.DocumentId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Custom((value, context) =>
                {
                    var document = _mediator.Send(new GetDocumentByIdQuery(value)).Result;

                    if (!document.Success)
                        context.AddFailure("DocumentId", "Document doesn't exist");

                    if (document.ReturnedObj != null && document.ReturnedObj.IsCompleted)
                        context.AddFailure("DocumentId", "Document is already completed");
                });

            RuleFor(ad => ad.DateOfOperationCompleted)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}
