namespace miniWms.Domain.Entities
{
    public class DocumentType
    {
        public string DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
