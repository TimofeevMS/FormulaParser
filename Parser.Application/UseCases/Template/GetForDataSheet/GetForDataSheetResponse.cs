using AutoMapper;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.Template.GetForDataSheet;

public record GetForDataSheetResponse
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public List<GetForDataSheetValue> Values { get; init; }
}

[AutoMap(typeof(TemplateAttribute))]
public record GetForDataSheetValue
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public string? Value { get; init; }
    
    public TemplateAttributeType Type { get; init; }
}