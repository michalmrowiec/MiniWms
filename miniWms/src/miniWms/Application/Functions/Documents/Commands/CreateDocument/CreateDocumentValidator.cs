using FluentValidation;
using MediatR;
using miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes;

namespace miniWms.Application.Functions.Documents.Commands.CreateDocument
{
    public class CreateDocumentValidator : AbstractValidator<CreateDocumentCommand>
    {
        private readonly IMediator _mediator;
        public CreateDocumentValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(d => d.DocumentTypeId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Custom((value, context) =>
                {
                    var roles = _mediator.Send(new GetAllDocumentTypesQuery()).Result;
                    if (!roles.Select(r => r.DocumentTypeId).Contains(value))
                        context.AddFailure("DocumentTypeId", "Document type doesn't exist");
                });

            RuleFor(d => d.Country)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(d => d.City)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(d => d.Region)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(d => d.PostalCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(20)
                .WithMessage("{PropertyName} must not exceed 20 characters");

            RuleFor(d => d.Address)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(250)
                .WithMessage("{PropertyName} must not exceed 250 characters");

            RuleForEach(d => d.DocumentEntries)
            .SetValidator(new CreateDocumentEntryValidator());
        }
    }
}
