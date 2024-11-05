using Parser.Domain.Entities;

namespace Parser.Domain.Interfaces;

public interface ITemplateEfCoreRepository : IEFCoreRepository<Template>
{
    Task<IEnumerable<TDto>> GetForMenuAsync<TDto>(CancellationToken cancellationToken = default);
}