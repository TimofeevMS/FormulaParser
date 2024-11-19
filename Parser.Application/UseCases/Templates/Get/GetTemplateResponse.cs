using AutoMapper;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.Templates.Get;

[AutoMap(typeof(Template), ReverseMap = true)]
public record GetTemplateResponse(Guid Id, string Name, List<GetTemplateAttribute> Attributes);

[AutoMap(typeof(TemplateAttribute), ReverseMap = true)]
public record GetTemplateAttribute(Guid Id, string Name, string Description, string? Formula, TemplateAttributeType Type);