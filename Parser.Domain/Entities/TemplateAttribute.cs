using Parser.Domain.Base;

namespace Parser.Domain.Entities;

public class TemplateAttribute : AuditableEntity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string? Formula { get; set; }
    
    public TemplateAttributeType Type { get; set; }
    
    public Guid TemplateId { get; set; }
    
    public Template Template { get; set; }
}