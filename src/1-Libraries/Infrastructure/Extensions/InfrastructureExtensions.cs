using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Infrastructure.Behaviors;
using CodeBlock.DevKit.Infrastructure.Bus;
using CodeBlock.DevKit.Infrastructure.Exceptions;
using CodeBlock.DevKit.Infrastructure.Mapping;
using CodeBlock.DevKit.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Infrastructure.Extensions;

public static class InfrastructureExtensions
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
        services.AddMediatR(handlerAssemblyMarkerType);
        services.AddInMemoryBus();
        services.AddApplicationExceptionHandlers();
        services.AddBehaviors(validatorAssemblyMarkerType, configuration);
        services.AddNotificationService();
        services.AddEncryptionService();
        services.AddEmailService();
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

    public static void AddEmailService(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
    }
}
