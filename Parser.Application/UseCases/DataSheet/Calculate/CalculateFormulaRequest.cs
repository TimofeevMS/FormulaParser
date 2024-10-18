using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.DataSheet.Calculate;

public record CalculateFormulaRequest : IRequest<Result<CalculateFormulaResponse>>
{
    public Guid? DataSheetId { get; init; }
    
    public Guid? ValueId { get; init; }
}