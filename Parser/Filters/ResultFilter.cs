using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Parser.Application.Common.Results;

namespace Parser.Filters;

public class ResultFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (resultContext.Result is ObjectResult objectResult)
        {
            var value = objectResult.Value;

            switch (value)
            {
                case Result { Succeeded: false } result:
                {
                    var problemDetails = MapToProblemDetails(result);
                    resultContext.Result = new ObjectResult(problemDetails)
                                           {
                                               StatusCode = problemDetails.Status
                                           };
                    return;
                }
                case Result { Succeeded: true } result:
                {
                    var data = ((dynamic)result).Data;

                    if (data is not null)
                    {
                        resultContext.Result = new ObjectResult(data)
                                               {
                                                   StatusCode = StatusCodes.Status200OK
                                               };
                    }
                    return;
                }
            }
        }
    }

    private ProblemDetails MapToProblemDetails(Result result)
    {
        var firstError = result.Errors.FirstOrDefault();
        return new ProblemDetails
        {
            Type = GetType(firstError.Type),
            Title = GetTitle(firstError),
            Detail = GetDetail(firstError),
            Status = GetStatusCode(firstError.Type),
            Extensions =
            {
                ["Errors"] = result.Errors.Select(e => new { e.Code, e.Description }).ToList()
            }
        };
    }
    
    private static string GetTitle(Error error)
    {
        return error.Type switch
               {
                   ErrorType.Conflict => error.Code,
                   ErrorType.Failure => error.Code,
                   ErrorType.Unexpected => error.Code,
                   ErrorType.Validation => error.Code,
                   ErrorType.NotFound => error.Code,
                   ErrorType.Forbidden => error.Code,
                   ErrorType.Unauthorized => error.Code,
                   _ => "Server failure"
               };
    }

    private static string GetDetail(Error error)
    {
        return error.Type switch
               {
                   ErrorType.Conflict => error.Description,
                   ErrorType.Failure => error.Description,
                   ErrorType.Unexpected => error.Description,
                   ErrorType.Validation => error.Description,
                   ErrorType.NotFound => error.Description,
                   ErrorType.Forbidden => error.Description,
                   ErrorType.Unauthorized => error.Description,
                   _ => "An unexpected error occurred"
               };
    }

    private static string GetType(ErrorType errorType)
    {
        return errorType switch
               {
                   ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                   ErrorType.Failure => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                   ErrorType.Unexpected => "https://tools.ietf.org/html/rfc7231",
                   ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                   ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                   ErrorType.Forbidden => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                   ErrorType.Unauthorized => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                   _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
               };
    }

    private static int GetStatusCode(ErrorType errorType)
    {
        return errorType switch
               {
                   ErrorType.Conflict => StatusCodes.Status409Conflict,
                   ErrorType.Failure => StatusCodes.Status400BadRequest,
                   ErrorType.Unexpected => StatusCodes.Status400BadRequest,
                   ErrorType.Validation => StatusCodes.Status400BadRequest,
                   ErrorType.NotFound => StatusCodes.Status404NotFound,
                   ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                   ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                   _ => StatusCodes.Status500InternalServerError
               };
    }
}
