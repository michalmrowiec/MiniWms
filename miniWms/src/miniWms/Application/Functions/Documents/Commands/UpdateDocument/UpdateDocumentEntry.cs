namespace miniWms.Application.Functions.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentEntry
    {
        public Guid DocumentEntryId { get; set; }
        public Guid ProductId { get; set; }
        public Guid DocumentId { get; set; }
        public int Quantity { get; set; }
    }
}
