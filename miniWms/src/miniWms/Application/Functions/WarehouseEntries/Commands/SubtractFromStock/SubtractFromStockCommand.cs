using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.WarehouseEntries.Commands.SubtractFromStock
{
    public record SubtractFromStockCommand(Guid WarehouseId, List<DocumentEntry> DocumentEntries) : IRequest<ResponseBase<List<WarehouseEntry>>>;
}
