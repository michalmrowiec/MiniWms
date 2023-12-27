using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IList<Role>>
    {
        private readonly IRolesRepository _roleRepository;
        public GetAllRolesQueryHandler(IRolesRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IList<Role>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleRepository.GetAllAsync();
        }
    }
}
