using Parser.Domain.Dto;

namespace Parser.Application.Interfaces;

public interface IFormulaService
{
    string? Evaluate(FormulaContext context);
}