using AutoMapper;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.DataSheets.GetTemplates;

public record GetTemplatesResponse(Guid Id, string Name, List<GetForDataSheetValue> Values);

[AutoMap(typeof(TemplateAttribute), ReverseMap = true)]
public record GetForDataSheetValue(Guid Id, string Name, string? Value, TemplateAttributeType Type);