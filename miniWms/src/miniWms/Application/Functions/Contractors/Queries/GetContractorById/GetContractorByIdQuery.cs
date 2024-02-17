using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Contractors.Queries.GetContractorById
{
    public record GetContractorByIdQuery(Guid ContractorId) : IRequest<ResponseBase<Contractor>>;
}
