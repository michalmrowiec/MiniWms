using FluentValidation;

namespace miniWms.Application.Functions.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentValidator : AbstractValidator<UpdateDocumentCommand>
    {
        public UpdateDocumentValidator()
        {
            RuleFor(d => d.Country)
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(d => d.City)
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(d => d.Region)
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(d => d.PostalCode)
                .MaximumLength(20)
                .WithMessage("{PropertyName} must not exceed 20 characters.");

            RuleFor(d => d.Address)
                .MaximumLength(250)
                .WithMessage("{PropertyName} must not exceed 250 characters.");
        }
    }
}
