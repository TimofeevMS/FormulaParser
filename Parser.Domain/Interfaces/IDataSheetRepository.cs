using Parser.Domain.Entities;

namespace Parser.Domain.Interfaces;

public interface IDataSheetRepository
{
    Task AddDataSheet(DataSheet dataSheet, CancellationToken cancellationToken = default);
    
    Task<DataSheet?> GetDataSheet(Guid requestId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TDto>> GetForMenuTemplates<TDto>(CancellationToken cancellationToken = default);
    
    Task UpdateDataSheet(DataSheet dataSheet, CancellationToken cancellationToken = default);
}