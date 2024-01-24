using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.ExternalDocuments.Commands
{
    public class CreateExternalDocumentCommand : CreateDocumentCommand, IRequest<ResponseBase<ExternalDocument>>
    {
        public Guid ContractorId { get; set; }
        public bool ContractorIsSupplier { get; set; }
    }
}
