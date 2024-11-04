using Parser.Application.Interfaces;
using Parser.Domain.Entities;

namespace Parser.Application.Services.Formula.VariableStrategy;

public class VariableLoaderFactory : IVariableLoaderFactory
{
    private readonly IEnumerable<IVariableLoaderStrategy> _strategies;

    public VariableLoaderFactory(IEnumerable<IVariableLoaderStrategy> strategies)
    {
        _strategies = strategies.ToList();
    }

    public IVariableLoaderStrategy GetStrategy(TemplateAttributeType type)
    {
        var strategy = _strategies.FirstOrDefault(s => (s.SupportedTypes & type) == type);
        if (strategy == null)
            throw new InvalidOperationException($"Стратегия для типа {type} не найдена");

        return strategy;
    }
}