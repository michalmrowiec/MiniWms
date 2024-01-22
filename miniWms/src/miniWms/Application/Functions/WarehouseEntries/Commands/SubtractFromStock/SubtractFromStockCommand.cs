using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.WarehouseEntries.Commands.SubtractFromStock
{
    public class SubtractFromStockCommand
    {
        public Guid WarehouseId { get; set; }
        public List<DocumentEntry> DocumentEntries { get; set; }
    }
}
