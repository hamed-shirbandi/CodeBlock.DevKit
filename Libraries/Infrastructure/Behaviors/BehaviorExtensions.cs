using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Infrastructure.Behaviors;

public static class BehaviorExtensions
{
    /// <summary>
    ///
    /// </summary>
    public static void AddApplicationBehaviors(this IServiceCollection services, Type validatorAssemblyMarkerType)
    {
        services.AddValidationBehaviour(validatorAssemblyMarkerType);
        services.AddCachingBehavior();
    }

    /// <summary>
    ///
    /// </summary>
    public static void AddValidationBehaviour(this IServiceCollection services, Type validatorAssemblyMarkerType)
    {
        //Load all fluent validation classes to be used in ValidationBehaviour
        services.AddValidatorsFromAssembly(validatorAssemblyMarkerType.Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }

    /// <summary>
    ///
    /// </summary>
    public static void AddCachingBehavior(this IServiceCollection services)
    {
        services.AddEasyCaching(option => option.UseInMemory());
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
    }
}
