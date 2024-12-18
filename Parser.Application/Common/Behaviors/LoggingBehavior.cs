﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace Parser.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, 
                                        RequestHandlerDelegate<TResponse> next, 
                                        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {RequestName} with data: {@Request}", typeof(TRequest).Name, request);

        try
        {
            var response = await next();

            _logger.LogInformation("Handled {RequestName} with response: {@Response}", typeof(TRequest).Name, response);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling {RequestName} with data: {@Request}", typeof(TRequest).Name, request);
            throw;
        }
    }
}