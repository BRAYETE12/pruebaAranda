namespace Application.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity item);
        Task DeleteAsync(TEntity item);
        Task<TEntity?> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> NoTrackin();
        Task<int> SaveChangesAsync();
    }
}
