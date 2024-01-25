using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes
{
    public record GetAllContractorsQuery() : IRequest<IList<Contractor>>;
}
