using FluentValidation;
using MediatR;

namespace miniWms.Application.Functions.Documents.ExternalDocuments.Commands
{
    public class CreateExternalDocumentValidator : CreateDocumentValidator<CreateExternalDocumentCommand>
    {
        public CreateExternalDocumentValidator(IMediator mediator) : base(mediator)
        {
            RuleFor(ed => ed.ContractorId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
            /*.Custom((value, context) =>
            {
                var roles = _mediator.Send(new GetAllDocumentTypesQuery()).Result;
                if (!roles.Select(r => r.DocumentTypeId).Contains(value))
                    context.AddFailure("DocumentTypeId", "Document type doesn't exist");
            });*/

            RuleFor(ed => ed.ContractorIsSupplier)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}
