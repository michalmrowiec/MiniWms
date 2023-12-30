using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class WarehousesRepository : CrudBaseRepository<Warehouse, Guid>, IWarehousesRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<Warehouse> _logger;

        public WarehousesRepository(MiniWmsDbContext context, ILogger<Warehouse> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
