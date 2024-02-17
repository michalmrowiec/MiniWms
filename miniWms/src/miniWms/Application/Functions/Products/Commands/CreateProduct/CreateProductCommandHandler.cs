using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseBase<Product>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMediator _mediator;
        public CreateProductCommandHandler(IProductsRepository productsRepository, IMediator mediator)
        {
            _productsRepository = productsRepository;
            _mediator = mediator;
        }

        public async Task<ResponseBase<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductValidator(_mediator);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new ResponseBase<Product>(validatorResult);
            }

            var product = new Product()
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                IsWeight = request.IsWeight,
                Unit = request.Unit,
                CategoryId = request.CategoryId,
                CreatedBy = request.CreatedBy,
                ModifiedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            var addedProduct = await _productsRepository.CreateAsync(product);

            return new ResponseBase<Product>(addedProduct);
        }
    }
}
