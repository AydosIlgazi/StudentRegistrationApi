using StudentRegistration.Application.Exceptions;
using System.Text.Json;
using FluentValidation.Results;

namespace StudentRegistration.Application.Behaviors;

public class ValidatonBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatonBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {


        var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            _logger.Error("Validation errors {HandlerName} \n Command: {@Command} \n Errors: {@ValidationErrors}",request.GetType().Name, request, failures);
            
            throw  new ValidationException<ValidationFailure>(failures);
        }


        return await next();
    }
}