namespace Parser.Domain.Entities;

public class DataSheetValue
{
    public Guid Id { get; set; }
    
    public Guid DataSheetId { get; set; }
    
    public DataSheet DataSheet { get; set; }
    
    public Guid TemplateAttributeId { get; set; }
    
    public TemplateAttribute TemplateAttribute { get; set; }
    
    public string? StringValue { get; private set; }
    
    public double? NumericValue { get; private set; }
    
    public bool? BooleanValue { get; private set; }
    
    public string? FormulaValue { get; private set; }

    public string? GetValue()
    {
        return TemplateAttribute.Type switch
        {
            TemplateAttributeType.String => StringValue,
            TemplateAttributeType.Number => NumericValue?.ToString(),
            TemplateAttributeType.Boolean => BooleanValue?.ToString(),
            TemplateAttributeType.Formula => FormulaValue,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public void SetValue(string? value)
    {
        switch (TemplateAttribute.Type)
        {
            case TemplateAttributeType.String:
                StringValue = value;
                break;
            case TemplateAttributeType.Number:
                NumericValue = Convert.ToDouble(value);
                break;
            case TemplateAttributeType.Boolean:
                BooleanValue = Convert.ToBoolean(value);
                break;
            case TemplateAttributeType.Formula:
                FormulaValue = value;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}