using Parser.Domain.Base;

namespace Parser.Domain.Entities;

public class DataSheet : AuditableEntity
{
    public string Name { get; set; }
    
    public Guid TemplateId { get; set; }
    
    public Template Template { get; set; }
    
    public List<DataSheetValue> Values { get; set; }
}