using System.Text.RegularExpressions;
using NCalc;
using Parser.Domain.Dto;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;

namespace  Parser.Domain.Services;

public class FormulaService : IFormulaService
{
    private readonly IEnumerable<ICustomFunctionHandler> _functionHandlers;

    public FormulaService(IEnumerable<ICustomFunctionHandler> functionHandlers)
    {
        _functionHandlers = functionHandlers;
    }

    public string? Evaluate(FormulaContext context)
    {
        var expression = new Expression(context.Formula, ExpressionOptions.IgnoreCaseAtBuiltInFunctions);
        var variables = ExtractVariables(context.Formula);
        
        foreach (var variable in variables)
        {
            LoadVariableIfNeeded(context.DataSheet, variable, context.VariableDefinitions);
            SetVariableValueInExpression(variable, expression, context.VariableDefinitions);
        }

        foreach (var handler in _functionHandlers)
            expression.EvaluateFunction += handler.HandleFunction;

        return expression.Evaluate()?.ToString();
    }

    private void LoadVariableIfNeeded(DataSheet dataSheet, string variable, IDictionary<string, (string FormulaOrValue, bool IsFormula)> variableDefinitions)
    {
        if (variableDefinitions.ContainsKey(variable))
            return;
        
        var variableData = LoadVariableDefinition(dataSheet, variable, variableDefinitions, new HashSet<string>());
        variableDefinitions[variable] = variableData;
    }

    private void SetVariableValueInExpression(string variable, Expression expression, IDictionary<string, (string FormulaOrValue, bool IsFormula)> variableDefinitions)
    {
        var (formulaOrValue, isFormula) = variableDefinitions[variable];

        if (isFormula)
        {
            var context = new FormulaContext
                          {
                              Formula = formulaOrValue,
                              VariableDefinitions = variableDefinitions
                          };
            
            var value =  Evaluate(context);
            expression.Parameters[variable] = value;
        }
        else
        {
            if (double.TryParse(formulaOrValue, out var numericValue))
            {
                expression.Parameters[variable] = numericValue;
            }
            else
            {
                throw new ArgumentException($"Значение переменной '{variable}' не является числовым: {formulaOrValue}");
            }
        }
    }

    private (string FormulaOrValue, bool IsFormula) LoadVariableDefinition(DataSheet dataSheet, string variableName, IDictionary<string, (string FormulaOrValue, bool IsFormula)> variableDefinitions, ISet<string> visitedVariables)
    {
        if (!visitedVariables.Add(variableName))
            throw new InvalidOperationException($"Цикличная зависимость обнаружена для переменной '{variableName}'");
        
        var variableData = dataSheet.Values.FirstOrDefault(v => v.TemplateAttribute.Name == variableName);

        if (variableData is null)
            return ("0", false);
        
        if (variableData.TemplateAttribute.Type is not TemplateAttributeType.Formula)
            return (variableData.GetValue(), false);

        var nestedVariables = ExtractVariables(variableData.TemplateAttribute.Formula);
            
        foreach (var nestedVariable in nestedVariables.Where(nestedVariable => !variableDefinitions.ContainsKey(nestedVariable)))
        {
            var nestedVariableData = LoadVariableDefinition(dataSheet, nestedVariable, variableDefinitions, visitedVariables);
            variableDefinitions[nestedVariable] = nestedVariableData;
        }

        return (variableData.TemplateAttribute.Formula, true);
    }

    private HashSet<string> ExtractVariables(string formula)
    {
        return new HashSet<string>(Regex.Matches(formula, @"[A-Za-z_][A-Za-z0-9_]*").Select(match => match.Value));
    }
}
