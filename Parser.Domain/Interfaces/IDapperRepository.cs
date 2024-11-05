namespace Parser.Domain.Interfaces;

public interface IDapperRepository<TEntity> : IRepository where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    void Update(TEntity entity);
    
    void Delete(TEntity entity);
}