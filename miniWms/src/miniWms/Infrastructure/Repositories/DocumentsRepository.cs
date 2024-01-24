using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;
using miniWms.Infrastructure.Repositories.Common;
using Sieve.Models;
using Sieve.Services;

namespace miniWms.Infrastructure.Repositories
{
    public class DocumentsRepository : CrudBaseRepository<Document, Guid, DocumentsRepository>, IDocumentsRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<DocumentsRepository> _logger;
        private readonly ISieveProcessor _sieveProcessor;

        public DocumentsRepository(MiniWmsDbContext context, ILogger<DocumentsRepository> logger, ISieveProcessor sieveProcessor) : base(context, logger)
        {
            _context = context;
            _logger = logger;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Document> GetByIdWithEntriesAsync(Guid id)
        {
            return await _context
                .Documents
                .Include(d => d.DocumentEntries)
                .FirstAsync(d => d.DocumentId.Equals(id));
        }

        public async Task<PagedResult<Document>> GetSortedAndFilteredAsync(SieveModel sieveModel)
        {
            var documents = _context.Documents
                .Include(p => p.DocumentEntries)
                .AsNoTracking()
                .AsQueryable();

            var filteredDocuments = await _sieveProcessor
                .Apply(sieveModel, documents)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, documents, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<Document>(filteredDocuments, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }
    }
}
