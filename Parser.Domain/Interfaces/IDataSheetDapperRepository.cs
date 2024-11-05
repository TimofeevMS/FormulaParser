using Parser.Domain.Entities;

namespace Parser.Domain.Interfaces;

public interface IDataSheetDapperRepository : IDapperRepository<DataSheet>
{
    Task<IEnumerable<TDto>> GetForMenuAsync<TDto>(CancellationToken cancellationToken = default);
}