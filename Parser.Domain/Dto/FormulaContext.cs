using System.Diagnostics.CodeAnalysis;
using Parser.Domain.Entities;

namespace Parser.Domain.Dto;

public class FormulaContext
{
    private string _formula;
    
    public DataSheet DataSheet { get; init; }

    public required string Formula
    {
        get => _formula;
        
        [MemberNotNull(nameof(_formula))]
        init => _formula = value.Replace(';', ',');
    }

    public required IDictionary<string, (string FormulaOrValue, bool IsFormula)> VariableDefinitions { get; init; }
}