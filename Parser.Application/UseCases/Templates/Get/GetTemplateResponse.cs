using AutoMapper;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.Templates.Get;

[AutoMap(typeof(Template), ReverseMap = true)]
public record GetTemplateResponse
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public List<GetTemplateAttribute> Attributes { get; init; }
}

[AutoMap(typeof(TemplateAttribute), ReverseMap = true)]
public record GetTemplateAttribute
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public string? Formula { get; init; }
    
    public TemplateAttributeType Type { get; init; }
}