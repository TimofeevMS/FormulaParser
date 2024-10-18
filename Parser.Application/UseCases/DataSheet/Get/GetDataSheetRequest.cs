using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.DataSheet.Get;

public record GetDataSheetRequest: IRequest<Result<GetDataSheetResponse>>
{
    public Guid? Id { get; init; }
}