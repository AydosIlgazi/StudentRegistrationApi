namespace StudentRegistration.WebApi.Middlewares;

using FluentValidation.Results;
using AutoMapper;
using Serilog;
using StudentRegistration.Application.Exceptions;
using StudentRegistration.Domain.Exceptions;
using System.Net;
using System.Text.Json;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            while(ex!=null && ex.GetType() == typeof(AutoMapperMappingException))
            {
                ex = ex.InnerException;
            };
            string result = string.Empty;
            var response = context.Response;
            if(ex.GetType()==typeof(StudentRegistrationDomainException))
            {
                _logger.Error(ex, "Domain Exception");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new { errors = ex?.Message });
            }
            else if (ex.GetType()==typeof(ValidationException<ValidationFailure>))
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                ValidationException<ValidationFailure> validationException = ex as ValidationException<ValidationFailure>;
                result = JsonSerializer.Serialize(new { errors = validationException?.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }) });
            }
            else
            {
                _logger.Error(ex, "Exception");
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result=JsonSerializer.Serialize(new { errors = "Server error, Please try again later" });
            }

            
            await response.WriteAsync(result);
        }
    }
}
