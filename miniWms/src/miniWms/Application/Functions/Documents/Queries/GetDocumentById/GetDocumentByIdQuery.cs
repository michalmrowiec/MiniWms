using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Queries.GetDocumentById
{
    public record GetDocumentByIdQuery(Guid DocumentId) : IRequest<ResponseBase<Document>>;
}
