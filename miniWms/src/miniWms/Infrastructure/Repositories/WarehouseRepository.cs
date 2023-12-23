using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Infrastructure.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public WarehouseRepository(MiniWmsDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse)
        {
            warehouse.WarehouseId = new Guid();

            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();

            var addedWArehouse = await _context.Warehouses
                .SingleOrDefaultAsync(w => w.WarehouseId.Equals(warehouse.WarehouseId)) ?? new();

            return addedWArehouse;
        }

        public Task<bool> DeleteWarehouseAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Warehouse>> GetAllWarehousesAsync()
        {
            var addedWArehouse = await _context.Warehouses.ToListAsync() ?? [];

            return addedWArehouse;
        }

        public Task<Warehouse> GetWarehouseByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Warehouse> UpdateWarehouseAsync(Warehouse Warehouse)
        {
            throw new NotImplementedException();
        }
    }
}
