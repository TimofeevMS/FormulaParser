namespace Parser.Domain.Entities;

public class Template
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public List<TemplateAttribute> Attributes { get; set; }
}