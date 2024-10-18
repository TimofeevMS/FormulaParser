using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.Template.GetForDataSheet;

public record GetForDataSheetRequest : IRequest<Result<GetForDataSheetResponse>>
{
    public Guid? Id { get; init; }
}