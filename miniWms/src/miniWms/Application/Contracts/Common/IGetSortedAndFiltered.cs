using miniWms.Domain.Models;
using Sieve.Models;

namespace miniWms.Application.Contracts.Common
{
    public interface IGetSortedAndFiltered<TEntity> where TEntity : class, new()
    {
        Task<PagedResult<TEntity>> GetSortedAndFilteredAsync(SieveModel sieveModel);

    }
}
