using Parser.Domain.Entities;

namespace Parser.Domain.Interfaces;

public interface ITemplateRepository
{
    Task AddTemplate(Template template, CancellationToken cancellationToken = default);
    
    Task<Template?> GetTemplate(Guid requestId, CancellationToken cancellationToken = default);
    
    Task UpdateTemplate(Template template, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Template>> GetAllTemplates(CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TDto>> GetForMenuTemplates<TDto>(CancellationToken cancellationToken = default);
}