using System.Text.RegularExpressions;
using NCalc;
using Parser.Application.Interfaces;
using Parser.Domain.Dto;
using Parser.Domain.Entities;

namespace  Parser.Application.Services.Formula;

public class FormulaService : IFormulaService
{
    private readonly IEnumerable<ICustomFunctionHandler> _functionHandlers;
    private readonly IVariableLoaderFactory _variableLoaderFactory;

    public FormulaService(IEnumerable<ICustomFunctionHandler> functionHandlers, IVariableLoaderFactory variableLoaderFactory)
    {
        _functionHandlers = functionHandlers;
        _variableLoaderFactory = variableLoaderFactory;
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

    private (string FormulaOrValue, bool IsFormula) LoadVariableDefinition(DataSheet dataSheet, string variableName,
                                                                           IDictionary<string, (string FormulaOrValue, bool IsFormula)> variableDefinitions,
                                                                           ISet<string> visitedVariables)
    {
        if (!visitedVariables.Add(variableName))
            throw new InvalidOperationException($"Цикличная зависимость обнаружена для переменной '{variableName}'");

        var variableData = dataSheet.Values.FirstOrDefault(v => v.TemplateAttribute.Name == variableName);
        var type = variableData?.TemplateAttribute.Type ?? TemplateAttributeType.Unknown;

        var strategy = _variableLoaderFactory.GetStrategy(type);

        var result = strategy.Load(dataSheet, variableName);

        if (!result.IsFormula)
            return result;
        
        var nestedVariables = ExtractVariables(result.FormulaOrValue);
        
        foreach (var nestedVariable in nestedVariables.Where(nv => !variableDefinitions.ContainsKey(nv)))
        {
            var nestedVariableData = LoadVariableDefinition(dataSheet, nestedVariable, variableDefinitions, visitedVariables);
            variableDefinitions[nestedVariable] = nestedVariableData;
        }

        return result;
    }

    private HashSet<string> ExtractVariables(string formula)
    {
        return new HashSet<string>(Regex.Matches(formula, @"[A-Za-z_][A-Za-z0-9_]*").Select(match => match.Value));
    }
}
