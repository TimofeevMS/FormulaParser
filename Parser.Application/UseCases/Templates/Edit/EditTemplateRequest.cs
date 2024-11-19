using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parser.Application.Common.Results;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.Templates.Edit;

public record EditTemplateRequest : IRequest<Result>
{
    [FromRoute]
    public Guid? Id { get; init; }
    
    [FromBody]
    public EditTemplateBody Body { get; init; }
}

[AutoMap(typeof(Template), ReverseMap = true)]
public record EditTemplateBody(string Name, List<EditTemplateAttribute> Attributes);

[AutoMap(typeof(TemplateAttribute), ReverseMap = true)]
public record EditTemplateAttribute(Guid? Id, string Name, string Description, string? Formula, TemplateAttributeType Type);