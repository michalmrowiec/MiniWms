using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseBase>
    {
        private readonly IProductsRepository _productsRepository;
        public CreateProductCommandHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<ResponseBase> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //TODO: add validator

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
