using AutoMapper;
using MediatR;
using Parser.Application.Common.Results;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.Templates.Create;

public record CreateTemplateRequest(string Name, IEnumerable<CreateTemplateAttribute> Attributes) : IRequest<Result<Guid>>;

[AutoMap(typeof(TemplateAttribute), ReverseMap = true)]
public record CreateTemplateAttribute(string Name, string Description, string? Formula, TemplateAttributeType Type);