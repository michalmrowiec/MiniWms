using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Warehouses.Queries.GetWarehouseById
{
    public record GetWarehouseByIdQuery(Guid WarehouseId) : IRequest<ResponseBase<Warehouse>>;
}
