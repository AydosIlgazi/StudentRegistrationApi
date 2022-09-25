using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace StudentRegistration.Infrastructure;

public static class ServiceRegistration
{
    public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StudentRegistrationContext>(opt => opt.UseSqlite(configuration.GetConnectionString("Database")));
    }
}
