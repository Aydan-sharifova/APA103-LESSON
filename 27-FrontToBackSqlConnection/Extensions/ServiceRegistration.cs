using _27_FrontToBackSqlConnection.Configurations;
using _27_FrontToBackSqlConnection.Services;
using Microsoft.Extensions.DependencyInjection;

namespace _27_FrontToBackSqlConnection.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddConfiguredServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        services.Configure<ServiceLifeTimeSettings>(configuration.GetSection(nameof(ServiceLifeTimeSettings)));

        ServiceLifeTimeSettings serviceLifeTimeSettings = new();
        configuration.GetSection(nameof(ServiceLifeTimeSettings)).Bind(serviceLifeTimeSettings);

        ServiceLifetime emailServiceLifetime = ParseLifetime(serviceLifeTimeSettings.EmailService);
        services.Add(new ServiceDescriptor(typeof(IEmailService), typeof(EmailService), emailServiceLifetime));

        return services;
    }

    private static ServiceLifetime ParseLifetime(string lifetime)
    {
        return lifetime.ToLowerInvariant() switch
        {
            "singleton" => ServiceLifetime.Singleton,
            "transient" => ServiceLifetime.Transient,
            _ => ServiceLifetime.Scoped
        };
    }
}
