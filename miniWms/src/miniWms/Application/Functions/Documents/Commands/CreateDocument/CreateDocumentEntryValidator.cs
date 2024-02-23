using FluentValidation;
using MediatR;
using miniWms.Application.Functions.Products.Queries.GetProductById;

namespace miniWms.Application.Functions.Documents.Commands.CreateDocument
{
    public class CreateDocumentEntryValidator : AbstractValidator<CreateDocumentEntry>
    {
        protected readonly IMediator _mediator;
        public CreateDocumentEntryValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(de => de.ProductId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .Custom((value, context) =>
                {
                    var product = _mediator.Send(new GetProductByIdQuery(value)).Result;
                    if (!product.Success)
                        context.AddFailure("ProductId", "Product doesn't exist.");
                });

            RuleFor(de => de.Quantity)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .GreaterThan(0)
                .WithMessage("{PropertyName} have to be greater than 0.");
        }
    }
}
