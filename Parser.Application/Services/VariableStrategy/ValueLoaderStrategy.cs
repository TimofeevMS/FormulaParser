using Parser.Application.Interfaces;
using Parser.Domain.Entities;

namespace Parser.Application.Services.VariableStrategy;

public class ValueLoaderStrategy : IVariableLoaderStrategy
{
    public TemplateAttributeType SupportedTypes => TemplateAttributeType.Number | TemplateAttributeType.Boolean | TemplateAttributeType.String;

    public (string FormulaOrValue, bool IsFormula) Load(DataSheet dataSheet, string variableName)
    {
        var variableData = dataSheet.Values.FirstOrDefault(v => v.TemplateAttribute.Name == variableName);
        if (variableData == null)
            return ("0", false);

        return (variableData.GetValue(), false);
    }
}