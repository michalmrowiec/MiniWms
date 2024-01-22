using miniWms.Application.Contracts.Common;
using miniWms.Domain.Entities;

namespace miniWms.Application.Contracts
{
    public interface IDocumentEntriesRepository : ICrudRepository<DocumentEntry, Guid>
    {
        Task<IList<DocumentEntry>> CreateRangeAsync(IList<DocumentEntry> entities);
    }
}
