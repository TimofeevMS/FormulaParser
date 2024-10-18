namespace Parser.Domain.Entities;

public class TemplateAttribute
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string? Formula { get; set; }
    
    public TemplateAttributeType Type { get; set; }
    
    public Guid DataSheetTemplateId { get; set; }
    
    public Template Template { get; set; }
}