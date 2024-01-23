using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class WarehouseEntriesRepository : CrudBaseRepository<WarehouseEntry, Guid, WarehouseEntriesRepository>, IWarehouseEntriesRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<WarehouseEntriesRepository> _logger;

        public WarehouseEntriesRepository(MiniWmsDbContext context, ILogger<WarehouseEntriesRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IList<WarehouseEntry>> GetForWarehouseAsync(Guid warehouseId)
        {
            return await _context.WarehouseEntries.AsNoTracking().Where(we => we.WarehouseId.Equals(warehouseId)).ToListAsync();
        }

        public async Task<IList<WarehouseEntry>> CreateRangeAsync(IList<WarehouseEntry> warehouseEntries)
        {
            await _context
                .WarehouseEntries
                .AddRangeAsync(warehouseEntries);

            await _context.SaveChangesAsync();

            return warehouseEntries;
        }

        public async Task<IList<WarehouseEntry>> UpdateRangeAsync(IList<WarehouseEntry> warehouseEntries)
        {
            _context
                .WarehouseEntries
                .UpdateRange(warehouseEntries);

            await _context.SaveChangesAsync();

            return warehouseEntries;
        }
    }
}
