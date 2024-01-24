using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.InternalDocuments.ApproveInternalDocument
{
    public class ApproveInternalDocumentCommand : IRequest<ResponseBase<InternalDocument>>
    {
        public Guid DocumentId { get; set; }

        public DateTime? DateOfOperationComplited { get; set; }
        public Guid? ModifiedBy { get; set; }

    }
}
