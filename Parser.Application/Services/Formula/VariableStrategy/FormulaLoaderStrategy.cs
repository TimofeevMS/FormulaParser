using Parser.Application.Interfaces;
using Parser.Domain.Entities;

namespace Parser.Application.Services.Formula.VariableStrategy;

public class FormulaLoaderStrategy : IVariableLoaderStrategy
{
    public TemplateAttributeType SupportedTypes => TemplateAttributeType.Formula;

    public (string FormulaOrValue, bool IsFormula) Load(DataSheet dataSheet, string variableName)
    {
        var variableData = dataSheet.Values.FirstOrDefault(v => v.TemplateAttribute.Name == variableName);
        if (variableData == null)
            return ("0", false);

        return (variableData.TemplateAttribute.Formula, true);
    }
}