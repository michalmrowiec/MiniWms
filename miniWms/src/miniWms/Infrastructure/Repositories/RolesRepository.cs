using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class RolesRepository : CrudBaseRepository<Role, string, RolesRepository>, IRolesRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<RolesRepository> _logger;

        public RolesRepository(MiniWmsDbContext context, ILogger<RolesRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
