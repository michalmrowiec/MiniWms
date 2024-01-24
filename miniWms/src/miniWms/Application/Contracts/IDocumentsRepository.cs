using miniWms.Application.Contracts.Common;
using miniWms.Domain.Entities;

namespace miniWms.Application.Contracts
{
    public interface IDocumentsRepository : ICrudRepository<Document, Guid>, IGetSortedAndFiltered<Document>
    {
        Task<Document> GetByIdWithEntriesAsync(Guid id);
    }
}
