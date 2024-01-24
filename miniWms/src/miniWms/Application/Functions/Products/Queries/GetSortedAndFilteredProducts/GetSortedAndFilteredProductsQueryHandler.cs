using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;

namespace miniWms.Application.Functions.Products.Queries.GetSortedAndFilteredProducts
{
    public class GetSortedAndFilteredProductsQueryHandler : IRequestHandler<GetSortedAndFilteredProductsQuery, PagedResult<Product>>
    {
        private readonly IProductsRepository _productsRepository;
        public GetSortedAndFilteredProductsQueryHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<PagedResult<Product>> Handle(GetSortedAndFilteredProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productsRepository.GetSortedAndFilteredAsync(request.SieveModel);
        }
    }
}
