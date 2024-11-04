namespace Parser.Application.UseCases.DataSheets.Calculate;

public record CalculateFormulaResponse
{
    public string? Result { get; init; }
}