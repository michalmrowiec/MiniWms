using FluentValidation;
using MediatR;
using miniWms.Application.Functions.Categories.Queries.GetCategoryById;

namespace miniWms.Application.Functions.Products.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IMediator _mediator;
        public CreateProductValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(p => p.ProductName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(250)
                .WithMessage("{PropertyName} must not exceed 250 characters");

            RuleFor(p => p.CategoryId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Custom((value, context) =>
                {
                    var category = _mediator.Send(new GetCategoryByIdQuery(value)).Result;
                    if (!category.Success)
                        context.AddFailure("MainWarehouseId", "Main warehouse doesn't exist");
                });

            RuleFor(p => p.Unit)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(20)
                .WithMessage("{PropertyName} must not exceed 250 characters");
        }
    }
}
