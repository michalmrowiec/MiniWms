using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Queries.GetDocumentById
{
    public record GetDocumentByIdWithEntriesQuery(Guid DocumentId) : IRequest<ResponseBase<Document>>;
}
