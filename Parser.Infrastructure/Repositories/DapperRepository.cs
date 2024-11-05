using System.Data;
using Dapper;
using Parser.Domain.Base;
using Parser.Domain.Interfaces;

namespace Parser.Infrastructure.Repositories;

public abstract class DapperRepository<TEntity> : IDapperRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IDbConnection _dbConnection;

    protected DapperRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = $"SELECT * FROM \"{typeof(TEntity).Name}\" WHERE \"Id\" = @Id";
        return await _dbConnection.QuerySingleOrDefaultAsync<TEntity>(query, new { Id = id });
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = $"SELECT * FROM \"{typeof(TEntity).Name}\"";
        return await _dbConnection.QueryAsync<TEntity>(query);
    }

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = $"SELECT COUNT(1) FROM \"{typeof(TEntity).Name}\" WHERE \"Id\" = @Id";
        return await _dbConnection.ExecuteScalarAsync<bool>(query, new { Id = id });
    }

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

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var columns = string.Join(", ", typeof(TEntity).GetProperties().Select(p => $"\"{p.Name}\""));
        var values = string.Join(", ", typeof(TEntity).GetProperties().Select(p => $"@{p.Name}"));
        var query = $"INSERT INTO \"{typeof(TEntity).Name}\" ({columns}) VALUES ({values})";
        
        await _dbConnection.ExecuteAsync(query, entity);
    }

    public virtual void Update(TEntity entity)
    {
        var setClause = string.Join(", ", typeof(TEntity).GetProperties().Select(p => $"\"{p.Name}\" = @{p.Name}"));
        var query = $"UPDATE \"{typeof(TEntity).Name}\" SET {setClause} WHERE \"Id\" = @Id";

        _dbConnection.Execute(query, entity);
    }

    public virtual void Delete(TEntity entity)
    {
        var query = $"DELETE FROM \"{typeof(TEntity).Name}\" WHERE \"Id\" = @Id";
        _dbConnection.Execute(query, new { Id = typeof(TEntity).GetProperty("Id")?.GetValue(entity) });
    }
}
