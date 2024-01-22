namespace miniWms.Application.Functions.DocumentEntries.Command
{
    public class CreateDocumentEntry
    {
        public Guid ProductId { get; set; }
        public Guid DocumentId { get; set; }
        public int Quantity { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
