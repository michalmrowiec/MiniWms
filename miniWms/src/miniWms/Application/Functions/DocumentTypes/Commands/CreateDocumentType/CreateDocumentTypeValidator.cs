using FluentValidation;

namespace miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType
{
    public class CreateDocumentTypeValidator : AbstractValidator<CreateDocumentTypeCommand>
    {
        public CreateDocumentTypeValidator()
        {
            RuleFor(dt => dt.DocumentTypeId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(3, 3)
                .WithMessage("{PropertyName} must be exactly 3 characters long");

            RuleFor(dt => dt.DocumentTypeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters");
        }
    }
}
