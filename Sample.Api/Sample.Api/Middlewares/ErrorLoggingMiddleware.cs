using System.Net;
using System.Text.Json;

namespace Sample.Api.Middlewares;

public class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _requestDelegate;
    private readonly ILogger<ErrorLoggingMiddleware> _logger;

    public ErrorLoggingMiddleware(RequestDelegate requestDelegate, ILoggerFactory loggerFactory)
    {
        _requestDelegate = requestDelegate;
        _logger = loggerFactory.CreateLogger<ErrorLoggingMiddleware>();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await HandleExceptionAsync(context);
        }
    }

    public static Task HandleExceptionAsync(HttpContext context)
    {
        var result = JsonSerializer.Serialize("An error occured.");
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(result);
    }
}
