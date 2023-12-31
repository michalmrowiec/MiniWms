using FluentValidation;

namespace miniWms.Application.Functions.Employees.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()

        {
            RuleFor(x => x.NewPassword)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MinimumLength(6)
                .WithMessage("{PropertyName} must be above 6 characters")
                .MaximumLength(35)
                .WithMessage("{PropertyName} must not exceed 35 characters");

            RuleFor(x => x.RepeatPassword)
                    .Equal(x => x.NewPassword)
                    .WithMessage("Passwords are not the same");
        }
    }
}
