using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace StudentRegistration.WebApi.Middlewares;

public class RequestContextLogMiddleware
{
    private readonly RequestDelegate _next;

    public RequestContextLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Request.Headers.TryGetValue("CorrelationId", out StringValues correlationId);
        if (correlationId.FirstOrDefault() != null)
        {
            using (LogContext.PushProperty("CorrelationId", correlationId.FirstOrDefault()))
            {
                await _next(context);
            }
        }
        else
        {
            await context.Response.WriteAsync("CorrelationId is missing in header");
        }

    }
}
