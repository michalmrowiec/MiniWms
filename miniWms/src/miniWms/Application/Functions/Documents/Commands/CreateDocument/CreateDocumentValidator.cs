using FluentValidation;

namespace miniWms.Application.Functions.Documents.Commands.CreateDocument
{
    public class CreateDocumentValidator : AbstractValidator<CreateDocumentCommand>
    {
        public CreateDocumentValidator()
        {
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
        }
    }
}
