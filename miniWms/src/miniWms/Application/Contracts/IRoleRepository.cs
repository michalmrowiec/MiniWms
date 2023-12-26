using miniWms.Domain.Entities;

namespace miniWms.Application.Contracts
{
    public interface IRoleRepository
    {
        Task<IList<Role>> GetAllRolesAsync();
    }
}
