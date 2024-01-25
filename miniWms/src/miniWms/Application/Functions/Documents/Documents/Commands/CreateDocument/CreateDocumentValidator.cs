using FluentValidation;
using MediatR;
using miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes;

namespace miniWms.Application.Functions.Documents.Documents.Commands.CreateDocument
{
    public class CreateDocumentValidator : AbstractValidator<CreateDocumentCommand>
    {
        protected readonly IMediator _mediator;
        public CreateDocumentValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(d => d.MainWarehouseId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required"); // validate warehouse exist

            RuleFor(d => d.TargetWarehouseId); // validate warehouse exist

            RuleFor(d => d.ContractorId); // validate warehouse exist

            RuleFor(d => d)
                .Custom((value, context) =>
                {
                    var documentTypes = _mediator.Send(new GetAllDocumentTypesQuery()).Result;

                    if (!documentTypes.Select(r => r.DocumentTypeId).Contains(value.DocumentTypeId))
                        context.AddFailure("DocumentTypeId", "Document type doesn't exist");

                    var documentType = documentTypes.FirstOrDefault(dt => dt.DocumentTypeId.Equals(value.DocumentTypeId));
                    if (documentType != null && !documentType.ActionType.Equals(value.ActionType))
                        context.AddFailure("ActionType", "Type of action doesn't exist");
                });

            RuleFor(d => d.Country)
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(d => d.City)
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(d => d.Region)
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(d => d.PostalCode)
                .MaximumLength(20)
                .WithMessage("{PropertyName} must not exceed 20 characters");

            RuleFor(d => d.Address)
                .MaximumLength(250)
                .WithMessage("{PropertyName} must not exceed 250 characters");

            RuleForEach(d => d.DocumentEntries)
            .SetValidator(new CreateDocumentEntryValidator());
        }
    }
}
