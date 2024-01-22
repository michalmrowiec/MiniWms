using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.DocumentEntries.Command.CreateDocumentEntries
{
    public class CreateDocumentEntriesCommand : IRequest<ResponseBase<List<DocumentEntry>>>
    {
        public List<CreateDocumentEntry> DocumentEntries { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
