using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parser.Application.Common.Results;
using Parser.Domain.Entities;

namespace Parser.Application.UseCases.DataSheets.Edit;

public record EditDataSheetRequest : IRequest<Result>
{
    [FromRoute]
    public Guid? Id { get; init; }
    
    [FromBody]
    public EditDataSheetBody Body { get; init; }
}

[AutoMap(typeof(DataSheet), ReverseMap = true)]
public record EditDataSheetBody(string Name, List<EditDataSheetValue> Values);

[AutoMap(typeof(DataSheetValue), ReverseMap = true)]
public record EditDataSheetValue(Guid? Id, string? Value);