using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public RoleRepository(MiniWmsDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IList<Role>> GetAllRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync() ?? [];

            return roles;
        }
    }
}
