using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.DataSheet.Get;

[AutoMap(typeof(Domain.Entities.DataSheet))]
public record GetDataSheetResponse
{
    public Guid Id { get; init; }
    
    public string? Name { get; init; }

    [SourceMember("Template.Name")]
    public string? TemplateName { get; init; }
    
    public List<GetDataSheetValue>  Values { get; init; }
}

[AutoMap(typeof(DataSheetValue))]
public record GetDataSheetValue
{
    public Guid Id { get; init; }
    
    [SourceMember("TemplateAttribute.Name")]
    public string Name { get; init; }
    
    public string? Value { get; init; }
    
    [SourceMember("TemplateAttribute.Formula")]
    public string? Formula { get; init; }
    
    [SourceMember("TemplateAttribute.Type")]
    public TemplateAttributeType Type { get; init; }
}