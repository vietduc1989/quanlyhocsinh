// QUAN-20260530-2301
using MediatR;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Interfaces;
using System.Diagnostics;

namespace ONENET.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly ICurrentUser _currentUser;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, ICurrentUser currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUser.UserId;
        var userName = _currentUser.UserName;

        _logger.LogInformation("Handling {RequestName} request for user {UserId} ({UserName}). Request: {@Request}",
            requestName, userId, userName, request);

        var timer = Stopwatch.StartNew();
        try
        {
            var response = await next();
            timer.Stop();
            _logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds}ms for user {UserId} ({UserName}). Response: {@Response}",
                requestName, timer.ElapsedMilliseconds, userId, userName, response);
            return response;
        }
        catch (Exception ex)
        {
            timer.Stop();
            _logger.LogError(ex, "Error handling {RequestName} in {ElapsedMilliseconds}ms for user {UserId} ({UserName}). Request: {@Request}",
                requestName, timer.ElapsedMilliseconds, userId, userName, request);
            throw;
        }
    }
}