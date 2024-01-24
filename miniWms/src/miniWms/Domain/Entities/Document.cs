using System.ComponentModel.DataAnnotations;

namespace miniWms.Domain.Entities
{
    public abstract class Document
    {
        public Guid DocumentId { get; set; }
        [MinLength(3)]
        [MaxLength(3)]
        public string DocumentTypeId { get; set; }
        public Guid WarehouseId { get; set; }
        public string? Comments { get; set; }
        public DateTime DateOfOperation { get; set; }
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
        public Warehouse? Warehouse { get; set; }
    }

    public class ExternalDocument : Document
    {
        public Guid ContractorId { get; set; }
        public bool ContractorIsSupplier { get; set; }

        public Contractor? Contractor { get; set; }
    }

    public class InternalDocument : Document
    {
        public Guid? TargetWarehouseId { get; set; }
        public bool IsComplited { get; set; }
        public bool IsReceived { get; set; }
        public bool IsStockTransfer { get; set; }
        public DateTime? DateOfOperationComplited { get; set; }

        public Warehouse? TargetWarehouse { get; set; }
    }
}
