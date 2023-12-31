namespace miniWms.Domain.Entities
{
    public class WarehouseEntry
    {
        public Guid WarehouseEntryId { get; set; }
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Employee? CreatedByEmployee { get; set; }
        public Employee? ModifiedByEmployee { get; set; }
        public Product? Product { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
