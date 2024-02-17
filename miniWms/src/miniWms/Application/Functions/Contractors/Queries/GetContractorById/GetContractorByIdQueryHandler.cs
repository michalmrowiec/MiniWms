using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Contractors.Queries.GetContractorById
{
    public class GetContractorByIdQueryHandler : IRequestHandler<GetContractorByIdQuery, ResponseBase<Contractor>>
    {
        private readonly IContractorsRepository _contractorsRepository;
        public GetContractorByIdQueryHandler(IContractorsRepository contractorsRepository)
        {
            _contractorsRepository = contractorsRepository;
        }
        public async Task<ResponseBase<Contractor>> Handle(GetContractorByIdQuery request, CancellationToken cancellationToken)
        {
            Contractor contractor;

            try
            {
                contractor = await _contractorsRepository.GetByIdAsync(request.ContractorId);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Contractor>(false, ex.Message);
            }

            return new ResponseBase<Contractor>(contractor);
        }
    }
}
