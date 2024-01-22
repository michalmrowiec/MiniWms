using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes
{
    public class GetAllDocumentTypesQueryHandler : IRequestHandler<GetAllDocumentTypesQuery, IList<DocumentType>>
    {
        private readonly IDocumentTypesRepository _documentTypesRepository;
        public GetAllDocumentTypesQueryHandler(IDocumentTypesRepository documentTypesRepository)
        {
            _documentTypesRepository = documentTypesRepository;
        }
        public async Task<IList<DocumentType>> Handle(GetAllDocumentTypesQuery request, CancellationToken cancellationToken)
        {
            return await _documentTypesRepository.GetAllAsync();
        }
    }
}
