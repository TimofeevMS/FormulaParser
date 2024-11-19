using AutoMapper;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.Templates.GetForMenu;

[AutoMap(typeof(Template), ReverseMap = true)]
public record GetForMenuTemplateResponse(string Name, Guid Id);