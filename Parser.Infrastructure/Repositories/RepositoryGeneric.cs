using Microsoft.EntityFrameworkCore;
using Parser.Domain.Base;
using Parser.Domain.Interfaces;

namespace Parser.Infrastructure.Repositories;

public abstract class RepositoryGeneric<TEntity> : IRepositoryGeneric<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;

    protected RepositoryGeneric(DbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
    
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) => await _dbSet.ToListAsync(cancellationToken);

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken) => await _dbSet.FindAsync([id], cancellationToken) is not null;

    public virtual async Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var idProperty = typeof(TEntity).GetProperty("Id");
        if (idProperty is not null)
        {
            var idValue = (Guid)idProperty.GetValue(entity)!;
            if (await ExistsAsync(idValue, cancellationToken))
                Update(entity);
            else
                await AddAsync(entity, cancellationToken);
        }
        else
            throw new InvalidOperationException("Entity must have an Id property.");
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) => await _dbSet.AddAsync(entity, cancellationToken);
    
    public virtual void Update(TEntity entity) => _dbSet.Update(entity);

    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}
