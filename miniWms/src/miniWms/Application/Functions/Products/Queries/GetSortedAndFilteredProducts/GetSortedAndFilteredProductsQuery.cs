using MediatR;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;
using Sieve.Models;

namespace miniWms.Application.Functions.Products.Queries.GetSortedAndFilteredProducts
{
    public record GetSortedAndFilteredProductsQuery(SieveModel SieveModel) : IRequest<PagedResult<Product>>;
}
