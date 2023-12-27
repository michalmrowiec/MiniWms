using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class WarehousesRepository : CrudBaseRepository<Warehouse, Guid>, IWarehousesRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<EmployeesRepository> _logger;

        public WarehousesRepository(MiniWmsDbContext context, ILogger<EmployeesRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
