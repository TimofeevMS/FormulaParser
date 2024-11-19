using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.DataSheets.Calculate;

public record CalculateFormulaRequest(Guid? DataSheetId, Guid? ValueId) : IRequest<Result<CalculateFormulaResponse>>;