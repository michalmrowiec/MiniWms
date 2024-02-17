namespace miniWms.Application.Contracts.Utilities
{
    public interface IUnitOfTransaction
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
