using AutoMapper;

namespace Parser.Application.UseCases.DataSheet.GetForMenu;

[AutoMap(typeof(Domain.Entities.DataSheet))]
public record GetForMenuDataSheetResponse
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
}