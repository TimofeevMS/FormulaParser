using Parser.Domain.Entities;

namespace Parser.Domain.Interfaces;

public interface ITemplateRepository : IRepositoryGeneric<Template>
{
    Task<IEnumerable<TDto>> GetForMenuAsync<TDto>(CancellationToken cancellationToken = default);
}