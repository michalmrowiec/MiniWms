using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.InternalDocuments.CreateInternalDocument
{
    public class CreateInternalDocumentCommand : CreateDocumentCommand, IRequest<ResponseBase<InternalDocument>>
    {
        public Guid? TargetWarehouseId { get; set; }
        public bool IsComplited { get; set; }
        public bool IsStockTransfer { get; set; } = true;
        public bool IsReceived { get; set; } = false;
        public DateTime? DateOfOperationComplited { get; set; }
    }
}
