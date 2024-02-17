namespace miniWms.Application.Contracts.Utilities
{
    public interface ITransactionManager
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
