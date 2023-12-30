using miniWms.Application.Contracts.Common;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;
using Sieve.Models;

namespace miniWms.Application.Contracts
{
    public interface IProductsRepository : ICrudRepository<Product, Guid>
    {
        Task<PagedResult<Product>> GetSortedAndFilteredProductsAsync(SieveModel sieveModel);
    }
}
