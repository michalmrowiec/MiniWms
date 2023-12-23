namespace miniWms.Domain.Entities
{
    public class Warehouse
    {
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Employee? CreatedByEmployee { get; set; }
        public Employee? ModifiedByEmployee { get; set; }
        public IList<WarehouseEntry> WarehouseEntries { get; set; } = new List<WarehouseEntry>();
        public IList<Document> DocumentsAsSupplier { get; set; } = new List<Document>();
        public IList<Document> DocumentsAsRecipient { get; set; } = new List<Document>();
    }
}
