using Parser.Domain.Entities;

namespace Parser.Application.Interfaces;

public interface IVariableLoaderStrategy
{
    TemplateAttributeType SupportedTypes { get; }
    
    (string FormulaOrValue, bool IsFormula) Load(DataSheet dataSheet, string variableName);
}