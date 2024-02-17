using FluentValidation;

namespace miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType
{
    public class CreateContractorValidator : AbstractValidator<CreateContractorCommand>
    {
        public CreateContractorValidator()
        {
            RuleFor(c => c.ContractorName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(250)
                .WithMessage("{PropertyName} must not exceed 250 characters"); ;

            RuleFor(c => c.VatId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(30)
                .WithMessage("{PropertyName} must not exceed 30 characters");

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

            RuleFor(c => c.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(20)
                .WithMessage("{PropertyName} must not exceed 20 characters"); ;

            RuleFor(c => c.EmailAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(255)
                .WithMessage("{PropertyName} must not exceed 255 characters")
                .EmailAddress()
                .WithMessage("{PropertyName} must be email address");
        }
    }
}
