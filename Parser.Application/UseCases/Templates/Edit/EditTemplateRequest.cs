﻿using AutoMapper;
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
public record EditTemplateBody
{
    public string Name { get; init; }
    
    public List<EditTemplateAttribute> Attributes { get; init; }
}

[AutoMap(typeof(TemplateAttribute), ReverseMap = true)]
public record EditTemplateAttribute
{
    public Guid? Id { get; init; }
    
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public string? Formula { get; init; }
    
    public TemplateAttributeType Type { get; init; }
}