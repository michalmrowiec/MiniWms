using FluentValidation;
using MediatR;
using miniWms.Application.Functions.Employees.Commands.Login;
using miniWms.Application.Functions.Employees.Queries.GetEmployeeByEmail;
using miniWms.Application.Functions.Roles.Queries.GetAllRoles;

namespace miniWms.Application.Functions.Employees.Commands.CreateEmployee
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(e => e.EmailAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .EmailAddress()
                .WithMessage("{PropertyName} must be email address");

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
