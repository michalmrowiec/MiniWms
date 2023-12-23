using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Warehouses.Queries.GetAllWarehouses
{
    public record GetAllWarehousesQuery() : IRequest<IList<Warehouse>>;
}
