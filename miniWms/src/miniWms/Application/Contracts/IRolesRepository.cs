using miniWms.Application.Contracts.Common;
using miniWms.Domain.Entities;

namespace miniWms.Application.Contracts
{
    public interface IRolesRepository : ICrudRepository<Role, string>
    {

    }
}
