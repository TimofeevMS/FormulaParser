using NCalc.Handlers;
using Parser.Application.Interfaces;

namespace Parser.Application.Services.Formula.CustomFunctions;

public class AndHandler : ICustomFunctionHandler
{
    public void HandleFunction(string name, FunctionArgs args)
    {
        if (!name.Equals("AND", StringComparison.OrdinalIgnoreCase))
            return;
        
        var result = args.Parameters.Select(p => Convert.ToBoolean(p.Evaluate())).All(b => b);
        
        args.Result = result;
    }
}