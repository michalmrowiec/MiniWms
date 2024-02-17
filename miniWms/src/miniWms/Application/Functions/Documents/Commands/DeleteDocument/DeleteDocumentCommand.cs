using MediatR;

namespace miniWms.Application.Functions.Documents.Commands.DeleteDocument
{
    public record DeleteDocumentCommand(Guid DocumentId) : IRequest<ResponseBase>;
}
