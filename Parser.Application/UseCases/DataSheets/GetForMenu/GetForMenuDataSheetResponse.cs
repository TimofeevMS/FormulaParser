using AutoMapper;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.DataSheets.GetForMenu;

[AutoMap(typeof(DataSheet), ReverseMap = true)]
public record GetForMenuDataSheetResponse(Guid Id, string Name);