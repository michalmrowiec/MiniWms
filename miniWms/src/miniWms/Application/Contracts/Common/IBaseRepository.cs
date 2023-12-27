namespace miniWms.Application.Contracts.Common
{
    public interface IBaseRepository<TEntity, TId> where TEntity : class, new()
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TId id);
        Task<IList<TEntity>> GetAllAsync();
    }
}
