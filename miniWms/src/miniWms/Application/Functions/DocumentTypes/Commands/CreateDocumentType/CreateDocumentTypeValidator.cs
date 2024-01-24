using FluentValidation;
using MediatR;
using miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes;

namespace miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType
{
    public class CreateDocumentTypeValidator : AbstractValidator<CreateDocumentTypeCommand>
    {
        private readonly IMediator _mediator;

        public CreateDocumentTypeValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(dt => dt.DocumentTypeId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(3, 3)
                .WithMessage("{PropertyName} must be exactly 3 characters long")
                .Custom((value, context) =>
                {
                    var documentTypes = _mediator.Send(new GetAllDocumentTypesQuery()).Result;
                    if(documentTypes.Select(dt => dt.DocumentTypeId).Contains(value))
                        context.AddFailure("DocumentTypeId", "Document type with this Id already exists.");
                });

            RuleFor(dt => dt.DocumentTypeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters")
                .Custom((value, context) =>
                {
                    var documentTypes = _mediator.Send(new GetAllDocumentTypesQuery()).Result;
                    if (documentTypes.Select(dt => dt.DocumentTypeName).Contains(value))
                        context.AddFailure("DocumentTypeName", "Document type with this name already exists.");
                });
        }
    }
}
