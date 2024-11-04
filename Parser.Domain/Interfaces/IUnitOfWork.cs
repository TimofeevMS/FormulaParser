namespace Parser.Domain.Interfaces;

public interface IUnitOfWork
{
    TRepository GetRepository<TRepository>() where TRepository : IRepository;
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}