using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class DocumentsRepository : CrudBaseRepository<Document, Guid, DocumentsRepository>, IDocumentsRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<DocumentsRepository> _logger;

        public DocumentsRepository(MiniWmsDbContext context, ILogger<DocumentsRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
