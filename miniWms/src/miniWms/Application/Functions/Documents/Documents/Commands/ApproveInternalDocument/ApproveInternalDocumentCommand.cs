using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Documents.Commands.ApproveInternalDocument
{
    public class ApproveInternalDocumentCommand : IRequest<ResponseBase<Document>>
    {
        public Guid DocumentId { get; set; }

        public DateTime? DateOfOperationComplited { get; set; }
        public Guid? ModifiedBy { get; set; }

    }
}
