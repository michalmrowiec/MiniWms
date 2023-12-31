using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class CategoryRepository : CrudBaseRepository<Category, Guid, CategoryRepository>, ICategoriesRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(MiniWmsDbContext context, ILogger<CategoryRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
