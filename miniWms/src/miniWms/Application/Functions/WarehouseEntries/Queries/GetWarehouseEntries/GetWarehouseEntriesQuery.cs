using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.WarehouseEntries.Queries.GetForWarehouse
{
    public record GetWarehouseEntriesQuery(Guid warehouseId) : IRequest<IList<WarehouseEntry>>;
}
