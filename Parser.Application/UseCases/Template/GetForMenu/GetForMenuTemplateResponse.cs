using AutoMapper;

namespace Parser.Application.UseCases.Template.GetForMenu;

[AutoMap(typeof(Domain.Entities.Template))]
public record GetForMenuTemplateResponse
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
}