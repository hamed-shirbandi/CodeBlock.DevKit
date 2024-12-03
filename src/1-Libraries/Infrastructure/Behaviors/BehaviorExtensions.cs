// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Infrastructure.Behaviors;

public static class BehaviorExtensions
{
    /// <summary>
    ///
    /// </summary>
    public static void AddBehaviors(this IServiceCollection services, Type validatorAssemblyMarkerType, IConfiguration configuration)
    {
        services.AddValidationBehaviour(validatorAssemblyMarkerType);
        services.AddCachingBehavior(configuration);
    }

    /// <summary>
    ///
    /// </summary>
    public static void AddValidationBehaviour(this IServiceCollection services, Type validatorAssemblyMarkerType)
    {
        if (validatorAssemblyMarkerType == null)
            return;

        //Load all fluent validation classes to be used in ValidationBehaviour
        services.AddValidatorsFromAssembly(validatorAssemblyMarkerType.Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }

    /// <summary>
    ///
    /// </summary>
    public static void AddCachingBehavior(this IServiceCollection services, IConfiguration configuration)
    {
        var cachingConfig = configuration.GetSection("Caching");
        if (cachingConfig == null)
            return;

        services.AddEasyCaching(option => option.UseInMemory());
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
    }
}
