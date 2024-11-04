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
public record EditDataSheetBody
{
    public string Name { get; init; }
    
    public List<EditDataSheetValue> Values { get; init; }
}

[AutoMap(typeof(DataSheetValue), ReverseMap = true)]
public record EditDataSheetValue
{
    public Guid? Id { get; init; }
    
    public string? Value { get; init; }
}