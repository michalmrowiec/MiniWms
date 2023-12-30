using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class CategoryRepository : CrudBaseRepository<Category, Guid>, ICategoriesRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<Category> _logger;

        public CategoryRepository(MiniWmsDbContext context, ILogger<Category> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
