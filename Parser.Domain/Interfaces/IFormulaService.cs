using Parser.Domain.Dto;

namespace Parser.Domain.Interfaces;

public interface IFormulaService
{
    string? Evaluate(FormulaContext context);
}