using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.WarehouseEntries.Commands.UpdateStock
{
    public record UpdateStockCommand(Document Document) : IRequest<ResponseBase<List<WarehouseEntry>>>;
}
