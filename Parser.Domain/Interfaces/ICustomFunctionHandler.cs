using NCalc.Handlers;

namespace Parser.Domain.Interfaces;

public interface ICustomFunctionHandler
{
    void HandleFunction(string name, FunctionArgs args);
}