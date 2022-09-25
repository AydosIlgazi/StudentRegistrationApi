using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace StudentRegistration.Application;

public static class ServiceRegistration
{
    public static void AddApplicationRegistration(this IServiceCollection services)
    {
        var assm = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assm);
        services.AddMediatR(assm);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatonBehavior<,>));
        services.AddValidatorsFromAssemblyContaining<CreateTermCommandValidator>();
    }
}
