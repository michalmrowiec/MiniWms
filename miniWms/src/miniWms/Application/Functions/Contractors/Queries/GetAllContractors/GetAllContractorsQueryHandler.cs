using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes
{
    public class GetAllContractorsQueryHandler : IRequestHandler<GetAllContractorsQuery, IList<Contractor>>
    {
        private readonly IContractorsRepository _contractorsRepository;
        public GetAllContractorsQueryHandler(IContractorsRepository contractorsRepository)
        {
            _contractorsRepository = contractorsRepository;
        }
        public async Task<IList<Contractor>> Handle(GetAllContractorsQuery request, CancellationToken cancellationToken)
        {
            return await _contractorsRepository.GetAllAsync();
        }
    }
}
