namespace miniWms.Domain.Entities
{
    public class DocumentEntry
    {
        public Guid DocumentEntryId { get; set; }
        public Guid ProductId { get; set; }
        public Guid DocumentId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Product? Product { get; set; }
    }
}
