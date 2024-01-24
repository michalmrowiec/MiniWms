using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;

namespace miniWms.Application.Functions.Documents.Queries.GetSortedAndFilteredDocuments
{
    public class GetSortedAndFilteredDocumentsQueryHandler : IRequestHandler<GetSortedAndFilteredDocumentsQuery, PagedResult<Document>>
    {
        private readonly IDocumentsRepository _documentsRepository;
        public GetSortedAndFilteredDocumentsQueryHandler(IDocumentsRepository documentsRepository)
        {
            _documentsRepository = documentsRepository;
        }
        public async Task<PagedResult<Document>> Handle(GetSortedAndFilteredDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await _documentsRepository.GetSortedAndFilteredAsync(request.SieveModel);
        }
    }
}
