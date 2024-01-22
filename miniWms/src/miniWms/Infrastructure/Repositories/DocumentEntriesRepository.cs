using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class DocumentEntriesRepository : CrudBaseRepository<DocumentEntry, Guid, DocumentEntriesRepository>, IDocumentEntriesRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<DocumentEntriesRepository> _logger;

        public DocumentEntriesRepository(MiniWmsDbContext context, ILogger<DocumentEntriesRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IList<DocumentEntry>> CreateRangeAsync(IList<DocumentEntry> entities)
        {
            await _context
                .DocumentEntries
                .AddRangeAsync(entities);

            await _context.SaveChangesAsync();

            return entities;
        }
    }
}
