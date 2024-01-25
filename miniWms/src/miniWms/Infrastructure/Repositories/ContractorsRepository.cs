using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories.Common;

namespace miniWms.Infrastructure.Repositories
{
    public class ContractorsRepository : CrudBaseRepository<Contractor, Guid, ContractorsRepository>, IContractorsRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<ContractorsRepository> _logger;

        public ContractorsRepository(MiniWmsDbContext context, ILogger<ContractorsRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
