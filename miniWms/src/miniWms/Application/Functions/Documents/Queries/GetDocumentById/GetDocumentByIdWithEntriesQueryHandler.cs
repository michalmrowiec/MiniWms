using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Queries.GetDocumentById
{
    public class GetDocumentByIdWithEntriesQueryHandler : IRequestHandler<GetDocumentByIdWithEntriesQuery, ResponseBase<Document>>
    {
        private readonly IDocumentsRepository _documentsRepository;
        public GetDocumentByIdWithEntriesQueryHandler(IDocumentsRepository documentsRepository)
        {
            _documentsRepository = documentsRepository;
        }
        public async Task<ResponseBase<Document>> Handle(GetDocumentByIdWithEntriesQuery request, CancellationToken cancellationToken)
        {
            Document document;
            try
            {
                document = await _documentsRepository.GetByIdWithEntriesAsync(request.DocumentId);

            }
            catch (Exception ex)
            {
                return new ResponseBase<Document>(false, "Something went wrong." + ex.Message);
            }

            return new ResponseBase<Document>(document);
        }
    }
}