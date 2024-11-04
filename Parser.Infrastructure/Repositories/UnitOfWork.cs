using Parser.Domain.Interfaces;
using Parser.Infrastructure.Contexts;

namespace Parser.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ParserDbContext _context;
    private readonly IEnumerable<IRepository> _repositories;

    public UnitOfWork(ParserDbContext context, IEnumerable<IRepository> repositories)
    {
        _context = context;
        _repositories = repositories;
    }

    public TRepository GetRepository<TRepository>() where TRepository : IRepository
    {
        var repository = _repositories.OfType<TRepository>().FirstOrDefault();
        if (repository == null)
        {
            throw new InvalidOperationException($"Repository of type {typeof(TRepository).Name} not found.");
        }
        return repository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
