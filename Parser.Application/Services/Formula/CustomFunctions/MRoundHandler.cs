using NCalc.Handlers;
using Parser.Application.Interfaces;

namespace Parser.Application.Services.Formula.CustomFunctions;

public class MRoundHandler : ICustomFunctionHandler
{
    public void HandleFunction(string name, FunctionArgs args)
    {
        if (!name.Equals("MROUND", StringComparison.OrdinalIgnoreCase))
            return;
        
        var value = (double)args.Parameters[0].Evaluate();
        var multiple = (int)args.Parameters[1].Evaluate();
        
        args.Result = Math.Round(value / multiple) * multiple;
    }
}