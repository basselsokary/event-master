using EventMaster.Application.Common.Interfaces.Services;
using EventMaster.Application.Hubs.Notification;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EventMaster.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}
