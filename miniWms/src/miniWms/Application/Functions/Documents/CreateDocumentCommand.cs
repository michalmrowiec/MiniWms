namespace miniWms.Application.Functions.Documents
{
    public class CreateDocumentCommand
    {
        public string DocumentTypeId { get; set; }
        public Guid WarehouseId { get; set; }
        public DateTime DateOfOperation { get; set; }
        public string? Comments { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public Guid? CreatedBy { get; set; }
        public List<CreateDocumentEntry> DocumentEntries { get; set; } = [];

    }
}
