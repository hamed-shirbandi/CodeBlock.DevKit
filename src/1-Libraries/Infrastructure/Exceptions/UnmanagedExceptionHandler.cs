// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Resources;
using MediatR.Pipeline;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace CodeBlock.DevKit.Infrastructure.Exceptions;

/// <summary>
/// Handle all unmanaged exceptions
/// </summary>
public class UnmanagedExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TException : Exception
{
    #region Fields


    private readonly INotificationService _notifications;
    private readonly ILogger<UnmanagedExceptionHandler<TRequest, TResponse, TException>> _logger;
    private readonly IStringLocalizer<CoreResource> _localizer;
    #endregion

    #region Ctors


    public UnmanagedExceptionHandler(
        INotificationService notifications,
        ILogger<UnmanagedExceptionHandler<TRequest, TResponse, TException>> logger,
        IStringLocalizer<CoreResource> localizer
    )
    {
        _notifications = notifications;
        _logger = logger;
        _localizer = localizer;
    }

    #endregion

    #region Handler



    /// <summary>
    ///
    /// </summary>
    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        _notifications.Add(_localizer[CoreResource.Unknown_Exception_Error]);

        _logger.LogError(exception, $"request : {JsonSerializer.Serialize(request)}");

        state.SetHandled(default);

        return Task.CompletedTask;
    }

    #endregion
}
