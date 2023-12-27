using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class WarehouseRepository : BaseRepository<Warehouse, Guid>, IWarehouseRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public WarehouseRepository(MiniWmsDbContext context, ILogger<EmployeeRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
