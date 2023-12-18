namespace miniWms.Domain.Entities
{
    public class Document
    {
        public Guid DocumentId { get; set; }
        public string DocumentTypeId { get; set; }
        public Guid? From { get; set; }
        public Guid? To { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public DocumentType? DocumentType { get; set; }
        public IList<DocumentEntry>? DocumentEntries { get; set; }
    }
}
