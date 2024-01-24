using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.WarehouseEntries.Commands.AddToStock
{
    public record AddToStockCommand(Guid WarehouseId, IList<DocumentEntry> DocumentEntries) : IRequest<ResponseBase<List<WarehouseEntry>>>;
}
