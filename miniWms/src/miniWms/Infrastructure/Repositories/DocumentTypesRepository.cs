using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class DocumentTypesRepository : CrudBaseRepository<DocumentType, string, DocumentTypesRepository>, IDocumentTypesRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<DocumentTypesRepository> _logger;

        public DocumentTypesRepository(MiniWmsDbContext context, ILogger<DocumentTypesRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
