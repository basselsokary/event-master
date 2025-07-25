using EventMaster.Application.Common.Behaviors;
using EventMaster.Application.Common.Interfaces.Services;
using EventMaster.Application.Hubs.Notification;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EventMaster.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            config.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
