using FluentValidation;

namespace miniWms.Application.Functions.Warehouses.Commands.CreateWarehouse
{
    public class CreateWarehouseValidator : AbstractValidator<CreateWarehouseCommand>
    {
        public CreateWarehouseValidator()
        {
            RuleFor(w => w.WarehouseName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(w => w.Country)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(w => w.City)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(w => w.Region)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(w => w.PostalCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(w => w.Address)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

        }
    }
}
