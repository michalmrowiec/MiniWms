using FluentValidation;
using MediatR;
using miniWms.Application.Functions.Documents.Queries.GetDocumentById;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Documents.Commands.ApproveInternalDocument
{
    public class ApproveInternalDocumentValidator : AbstractValidator<ApproveInternalDocumentCommand>
    {
        private readonly IMediator _mediator;
        public ApproveInternalDocumentValidator(IMediator mediator)
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

                    if (document.ReturnedObj is not Document internalDocument)
                        context.AddFailure("DocumentId", "Document is not an Internal Document");
                    else if (internalDocument.IsComplited)
                        context.AddFailure("DocumentId", "Document is already completed");
                }); // TODO fix

            RuleFor(ad => ad.DateOfOperationComplited)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}
