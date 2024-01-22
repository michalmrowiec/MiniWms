using System.ComponentModel.DataAnnotations;

namespace miniWms.Domain.Entities
{
    public class Document
    {
        public Guid DocumentId { get; set; }
        [MinLength(3)]
        [MaxLength(3)]
        public string DocumentTypeId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? RecipientId { get; set; }
        [MaxLength(100)]
        public string? Country { get; set; }
        [MaxLength(100)]
        public string? City { get; set; }
        [MaxLength(100)]
        public string? Region { get; set; }
        [MaxLength(20)]
        public string? PostalCode { get; set; }
        [MaxLength(250)]
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Employee? CreatedByEmployee { get; set; }
        public Employee? ModifiedByEmployee { get; set; }
        public DocumentType? DocumentType { get; set; }
        public IList<DocumentEntry> DocumentEntries { get; set; } = new List<DocumentEntry>();
        public Contractor? ContractorSupplier { get; set; }
        public Contractor? ContractorRecipient { get; set; }
        public Warehouse? WarehouseSupplier { get; set; }
        public Warehouse? WarehouseRecipient { get; set; }
    }
}
