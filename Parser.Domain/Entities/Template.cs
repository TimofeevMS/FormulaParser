using Parser.Domain.Base;

namespace Parser.Domain.Entities;

public class Template : AuditableEntity
{
    public string Name { get; set; }
    
    public List<TemplateAttribute> Attributes { get; set; }
}