using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Roles.Queries.GetAllRoles
{
    public record GetAllRolesQuery() : IRequest<IList<Role>>;
}
