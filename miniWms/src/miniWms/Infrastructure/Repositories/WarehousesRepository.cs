using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class WarehousesRepository : CrudBaseRepository<Warehouse, Guid, WarehousesRepository>, IWarehousesRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<WarehousesRepository> _logger;

        public WarehousesRepository(MiniWmsDbContext context, ILogger<WarehousesRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
