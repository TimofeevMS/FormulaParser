using AutoMapper;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.Template.Get;

[AutoMap(typeof(Domain.Entities.Template))]
public record GetTemplateResponse
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public List<GetTemplateAttribute> Attributes { get; init; }
}

[AutoMap(typeof(TemplateAttribute))]
public record GetTemplateAttribute
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public string? Formula { get; init; }
    
    public TemplateAttributeType Type { get; init; }
}