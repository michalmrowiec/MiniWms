using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.WarehouseEntries.Commands.AddToStock
{
    public class AddToStockCommand
    {
        public Guid WarehouseId { get; set; }
        public List<DocumentEntry> DocumentEntries { get; set; }

    }
}
