using Parser.Domain.Entities;

namespace Parser.Domain.Interfaces;

public interface IDataSheetRepository : IRepositoryGeneric<DataSheet>
{
    Task<IEnumerable<TDto>> GetForMenuAsync<TDto>(CancellationToken cancellationToken = default);
}