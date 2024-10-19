namespace Parser.Domain.Entities;

[Flags]
public enum TemplateAttributeType
{
    Unknown,
    String,
    Number,
    Boolean,
    Formula,
}