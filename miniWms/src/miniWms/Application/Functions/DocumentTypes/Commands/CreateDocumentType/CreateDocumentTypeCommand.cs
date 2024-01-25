using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType
{
    public class CreateDocumentTypeCommand : IRequest<ResponseBase<DocumentType>>
    {
        public string DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public ActionType ActionType { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
