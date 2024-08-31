using CodeBlock.DevKit.Infrastructure.Behaviors;
using CodeBlock.DevKit.Infrastructure.Bus;
using CodeBlock.DevKit.Infrastructure.Exceptions;
using CodeBlock.DevKit.Infrastructure.Notifications;
using CodeBlock.DevKit.Infrastructure.Security;
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
        Type validatorAssemblyMarkerType
    )
    {
        services.AddApplicationExceptionHandlers();
        services.AddApplicationBehaviors(validatorAssemblyMarkerType);
        services.AddNotificationService();
        services.AddInMemoryBus(handlerAssemblyMarkerType);
        services.AddEncryptionService();
    }
}
