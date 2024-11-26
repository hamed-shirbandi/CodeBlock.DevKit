using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Infrastructure.Behaviors;
using CodeBlock.DevKit.Infrastructure.Exceptions;
using CodeBlock.DevKit.Infrastructure.Mapping;
using CodeBlock.DevKit.Infrastructure.Models;
using CodeBlock.DevKit.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Infrastructure;

public static class Startup
{
    /// <summary>
    ///
    /// </summary>
    public static void AddCodeBlockDevKitInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType = null,
        Type mappingProfileMarkerType = null
    )
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterGenericHandlers = false;
            cfg.RegisterServicesFromAssemblyContaining(handlerAssemblyMarkerType);
            cfg.RegisterServicesFromAssemblyContaining(typeof(ManagedExceptionHandler<,,>));
        });
        services.AddMediatRDispatcher();
        services.AddExceptionHandlers();
        services.AddBehaviors(validatorAssemblyMarkerType, configuration);
        services.AddNotificationService();
        services.AddEncryptionService();
        services.AddEmailService(configuration);
        services.AddMapper(mappingProfileMarkerType);
    }

    public static void AddNotificationService(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();
    }

    public static void AddEncryptionService(this IServiceCollection services)
    {
        services.AddScoped<IEncryptionService, EncryptionService>();
    }

    public static void AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        var EmailConfig = configuration.GetSection("Email");
        if (!EmailConfig.Exists())
            return;

        Action<EmailOptions> setupAction = EmailConfig.Bind;

        services.Configure(setupAction);
        services.AddScoped<IEmailService, EmailService>();
    }

    private static void AddMediatRDispatcher(this IServiceCollection services)
    {
        services.AddScoped<IRequestDispatcher, MediatRDispatcher>();
    }
}
