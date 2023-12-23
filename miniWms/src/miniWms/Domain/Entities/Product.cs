namespace miniWms.Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public Guid CategoryId { get; set; }
        public string Unit { get; set; }
        public bool IsWeight { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Employee? CreatedByEmployee { get; set; }
        public Employee? ModifiedByEmployee { get; set; }
        public Category? Category { get; set; }
        public IList<DocumentEntry> DocumentEntries { get; set; } = new List<DocumentEntry>();
        public IList<WarehouseEntry> WarehouseEntries { get; set; } = new List<WarehouseEntry>();

    }
}
