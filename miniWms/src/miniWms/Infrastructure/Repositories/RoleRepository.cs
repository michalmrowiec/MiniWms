using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role, string>, IRoleRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public RoleRepository(MiniWmsDbContext context, ILogger<EmployeeRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
