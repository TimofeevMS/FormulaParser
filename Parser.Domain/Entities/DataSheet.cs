namespace Parser.Domain.Entities;

public class DataSheet
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public Guid DataSheetTemplateId { get; set; }
    
    public Template Template { get; set; }
    
    public List<DataSheetValue> Values { get; set; }
}