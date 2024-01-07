using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class CategoriesRepository : CrudBaseRepository<Category, Guid, CategoriesRepository>, ICategoriesRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<CategoriesRepository> _logger;

        public CategoriesRepository(MiniWmsDbContext context, ILogger<CategoriesRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
