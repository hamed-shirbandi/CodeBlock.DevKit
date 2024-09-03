using CodeBlock.DevKit.Infrastructure.Behaviors;
using CodeBlock.DevKit.Infrastructure.Bus;
using CodeBlock.DevKit.Infrastructure.Exceptions;
using CodeBlock.DevKit.Infrastructure.Notifications;
using CodeBlock.DevKit.Infrastructure.Security;
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
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType,
        IConfiguration configuration
    )
    {
        services.AddInMemoryBus(handlerAssemblyMarkerType);
        services.AddApplicationExceptionHandlers();
        services.AddBehaviors(validatorAssemblyMarkerType, configuration);
        services.AddNotificationService();
        services.AddEncryptionService();
    }
}
