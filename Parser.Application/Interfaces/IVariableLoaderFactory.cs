using Parser.Domain.Entities;

namespace Parser.Application.Interfaces;

public interface IVariableLoaderFactory
{
    IVariableLoaderStrategy GetStrategy(TemplateAttributeType type);
}