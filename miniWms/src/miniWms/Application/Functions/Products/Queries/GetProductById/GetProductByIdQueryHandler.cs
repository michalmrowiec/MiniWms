using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ResponseBase<Product>>
    {
        private readonly IProductsRepository _productsRepository;
        public GetProductByIdQueryHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<ResponseBase<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product product;

            try
            {
                product = await _productsRepository.GetByIdAsync(request.ProductId);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Product>(false, ex.Message);
            }

            return new ResponseBase<Product>(product);
        }
    }
}
