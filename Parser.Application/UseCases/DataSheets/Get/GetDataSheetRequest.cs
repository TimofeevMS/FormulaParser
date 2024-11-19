using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.DataSheets.Get;

public record GetDataSheetRequest(Guid? Id) : IRequest<Result<GetDataSheetResponse>>;