using Microsoft.EntityFrameworkCore.Storage;
using miniWms.Application.Contracts.Utilities;

namespace miniWms.Infrastructure.Utilities
{
    public class EfTransactionManager : ITransactionManager
    {
        private readonly MiniWmsDbContext _context;
        private IDbContextTransaction _transaction;

        public EfTransactionManager(MiniWmsDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
