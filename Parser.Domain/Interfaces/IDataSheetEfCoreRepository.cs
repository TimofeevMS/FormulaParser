using Parser.Domain.Entities;

namespace Parser.Domain.Interfaces;

public interface IDataSheetEfCoreRepository : IEFCoreRepository<DataSheet>
{
    Task<IEnumerable<TDto>> GetForMenuAsync<TDto>(CancellationToken cancellationToken = default);
}