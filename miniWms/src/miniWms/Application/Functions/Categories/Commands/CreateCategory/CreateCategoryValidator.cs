using FluentValidation;
using miniWms.Application.Functions.Categories.Commands.CreateCategory;

namespace miniWms.Application.Functions.Warehouses.Commands.CreateWarehouse
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.CategoryName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}
