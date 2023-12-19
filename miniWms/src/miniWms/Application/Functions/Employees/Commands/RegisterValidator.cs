using FluentValidation;
using MediatR;
using miniWms.Application.Functions.Employees.Queries.GetEmployeeByEmail;

namespace miniWms.Application.Functions.Employees.Commands
{
    public class RegisterValidator : AbstractValidator<AddEmployeeCommand>
    {
        private readonly IMediator _mediator;

        public RegisterValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(r => r.EmailAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .EmailAddress()
                .WithMessage("{PropertyName} must be email address")
                .Custom((value, context) =>
                {
                    var user = _mediator.Send(new GetEmployeeByEmailQuery(value)).Result;
                    if (user.EmailAddress != null)
                        context.AddFailure("Email", "Email is taken");
                });

            RuleFor(r => r.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MinimumLength(6)
                .WithMessage("{PropertyName} must be above 6 characters")
                .MaximumLength(35)
                .WithMessage("{PropertyName} must not exceed 35 characters");
        }
    }
}
