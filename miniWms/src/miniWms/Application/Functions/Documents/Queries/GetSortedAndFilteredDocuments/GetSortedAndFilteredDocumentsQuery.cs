using MediatR;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;
using Sieve.Models;

namespace miniWms.Application.Functions.Documents.Queries.GetSortedAndFilteredDocuments
{
    public record GetSortedAndFilteredDocumentsQuery(SieveModel SieveModel) : IRequest<PagedResult<Document>>;
}
