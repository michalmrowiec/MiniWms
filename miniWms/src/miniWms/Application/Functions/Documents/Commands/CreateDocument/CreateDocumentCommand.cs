using MediatR;
using miniWms.Application.Functions.DocumentEntries.Command;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommand : IRequest<ResponseBase<Document>>
    {
        public string DocumentTypeId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? RecipientId { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public Guid? CreatedBy { get; set; }
        public List<CreateDocumentEntry> DocumentEntries { get; set; } = [];

    }
}
