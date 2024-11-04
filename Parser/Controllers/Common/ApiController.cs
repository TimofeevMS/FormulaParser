using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parser.Application.Common.Results;

namespace Parser.Controllers.Common;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected ISender Sender => HttpContext.RequestServices.GetRequiredService<ISender>();

    [ApiExplorerSettings(IgnoreApi = true)]
    protected static IResult ToResponse(Result result)
    {
        if (result.Succeeded)
            return TypedResults.Ok(result);

        if (result.Errors.Length > 1)
            return TypedResults.BadRequest(result);
        
        var error = result.Errors.First();

        return error.Type switch
               {
                   ErrorType.Conflict => TypedResults.Conflict(result),
                   ErrorType.Failure => TypedResults.BadRequest(result),
                   ErrorType.Unexpected => TypedResults.BadRequest(result),
                   ErrorType.Validation => TypedResults.BadRequest(result),
                   ErrorType.NotFound => TypedResults.NotFound(result),
                   ErrorType.Forbidden => TypedResults.Forbid(),
                   ErrorType.Unauthorized => TypedResults.Unauthorized(),
                   _ => TypedResults.Empty
               };
    }
}