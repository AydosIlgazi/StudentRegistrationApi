

namespace StudentRegistration.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;

    public LoggingBehavior(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.Information("Handler Name {HandlerName} \n Request: {@Request}", request.GetType().Name,request);

        var response = await next();

        _logger.Information("Handler Name {HandlerName} \n Response: {@Response}", request.GetType().Name, response);

        return response;

    }
}
