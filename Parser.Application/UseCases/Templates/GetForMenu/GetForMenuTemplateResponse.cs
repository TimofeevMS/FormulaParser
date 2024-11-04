using AutoMapper;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.Templates.GetForMenu;

[AutoMap(typeof(Template), ReverseMap = true)]
public record GetForMenuTemplateResponse
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
}