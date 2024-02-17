using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Commands.ApproveInternalDocument
{
    public class ApproveDocumentCommand : IRequest<ResponseBase<Document>>
    {
        public Guid DocumentId { get; set; }

        public DateTime? DateOfOperationComplited { get; set; }
        public Guid? ModifiedBy { get; set; }

    }
}
