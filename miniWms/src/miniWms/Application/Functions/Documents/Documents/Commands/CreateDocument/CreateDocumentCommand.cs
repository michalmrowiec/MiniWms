using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommand : IRequest<ResponseBase<Document>>
    {
        public string DocumentTypeId { get; set; }
        public ActionType ActionType { get; set; }
        public Guid MainWarehouseId { get; set; }
        public Guid? ContractorId { get; set; }
        public Guid? TargetWarehouseId { get; set; }
        public bool IsComplited { get; set; }
        public DateTime DateOfOperation { get; set; }
        public DateTime? DateOfOperationComplited { get; set; }
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
