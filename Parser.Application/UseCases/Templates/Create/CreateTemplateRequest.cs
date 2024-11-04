using AutoMapper;
using MediatR;
using Parser.Application.Common.Results;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.Templates.Create;

public record CreateTemplateRequest : IRequest<Result<Guid>>
{
    public string Name { get; init; }
    
    public IEnumerable<CreateTemplateAttribute> Attributes { get; init; }
}

[AutoMap(typeof(TemplateAttribute), ReverseMap = true)]
public record CreateTemplateAttribute
{
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public string? Formula { get; init; }
    
    public TemplateAttributeType Type { get; init; }
}