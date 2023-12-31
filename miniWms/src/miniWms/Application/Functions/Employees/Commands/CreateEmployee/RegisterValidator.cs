using FluentValidation;
using MediatR;
using miniWms.Application.Functions.Employees.Queries.GetEmployeeByEmail;
using miniWms.Application.Functions.Roles.Queries.GetAllRoles;

namespace miniWms.Application.Functions.Employees.Commands.CreateEmployee
{
    public class RegisterValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private readonly IMediator _mediator;

        public RegisterValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.RoleId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Custom((value, context) =>
                {
                    var roles = _mediator.Send(new GetAllRolesQuery()).Result;
                    if (!roles.Select(r => r.RoleId).Contains(value))
                        context.AddFailure("RoleId", "Role doesn't exist");
                });

            RuleFor(e => e.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.City)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.PostalCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.Region)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.Country)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.Address)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.HaveToChangePassword)
                .NotNull()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.IsActive)
                .NotNull()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.EmailAddress)
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

            RuleFor(e => e.Password)
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
