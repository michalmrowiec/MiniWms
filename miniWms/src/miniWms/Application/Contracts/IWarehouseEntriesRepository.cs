using miniWms.Application.Contracts.Common;
using miniWms.Domain.Entities;

namespace miniWms.Application.Contracts
{
    public interface IWarehouseEntriesRepository : ICrudRepository<WarehouseEntry, Guid>
    {
        Task<IList<WarehouseEntry>> GetForWarehouseAsync(Guid warehouseId);
        Task<IList<WarehouseEntry>> CreateRangeAsync(IList<WarehouseEntry> warehouseEntries);
        Task<IList<WarehouseEntry>> UpdateRangeAsync(IList<WarehouseEntry> warehouseEntries);
    }
}
