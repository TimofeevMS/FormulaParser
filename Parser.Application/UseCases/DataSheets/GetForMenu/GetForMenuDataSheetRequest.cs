using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.DataSheets.GetForMenu;

public record GetForMenuDataSheetRequest : IRequest<Result<IEnumerable<GetForMenuDataSheetResponse>>>;