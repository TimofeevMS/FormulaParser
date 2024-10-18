using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.DataSheet.GetForMenu;

public record GetForMenuDataSheetRequest : IRequest<Result<IEnumerable<GetForMenuDataSheetResponse>>>;