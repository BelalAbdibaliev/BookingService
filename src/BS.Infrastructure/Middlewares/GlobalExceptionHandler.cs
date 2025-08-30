using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BS.Infrastructure.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Произошла ошибка");

        var problemDetails = new ProblemDetails
        {
            Title = "Ошибка",
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };

        // 👇 Маппим разные исключения на разные статусы
        problemDetails.Status = exception switch
        {
            ArgumentNullException        => StatusCodes.Status400BadRequest,
            InvalidOperationException    => StatusCodes.Status400BadRequest,
            KeyNotFoundException         => StatusCodes.Status404NotFound,
            _                            => StatusCodes.Status500InternalServerError
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}