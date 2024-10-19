using NCalc.Handlers;

namespace Parser.Application.Interfaces;

public interface ICustomFunctionHandler
{
    void HandleFunction(string name, FunctionArgs args);
}