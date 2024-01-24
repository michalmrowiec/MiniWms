using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.WarehouseEntries.Queries.GetForWarehouse
{
    public record GetForWarehouseQuery(Guid warehouseId) : IRequest<IList<WarehouseEntry>>;
}
