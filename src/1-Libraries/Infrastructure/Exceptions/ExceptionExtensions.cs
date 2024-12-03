// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Infrastructure.Exceptions;

public static class ExceptionExtensions
{
    /// <summary>
    ///
    /// </summary>
    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddManagedExceptionsHandler();
        services.AddUnmanagedExceptionsHandler();
    }

    /// <summary>
    ///
    /// </summary>
    private static void AddManagedExceptionsHandler(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRequestExceptionHandler<,,>), typeof(ManagedExceptionHandler<,,>));
    }

    /// <summary>
    ///
    /// </summary>
    private static void AddUnmanagedExceptionsHandler(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRequestExceptionHandler<,,>), typeof(UnmanagedExceptionHandler<,,>));
    }
}
