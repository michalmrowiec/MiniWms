namespace miniWms.Application.Contracts.Common
{
    public interface ICrudRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TId id);
        Task<IList<TEntity>> GetAllAsync();
    }
}
