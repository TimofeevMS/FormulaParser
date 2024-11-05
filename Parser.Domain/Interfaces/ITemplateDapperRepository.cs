using Parser.Domain.Entities;

namespace Parser.Domain.Interfaces;

public interface ITemplateDapperRepository : IDapperRepository<Template>
{
    Task<IEnumerable<TDto>> GetForMenuAsync<TDto>(CancellationToken cancellationToken = default);
}