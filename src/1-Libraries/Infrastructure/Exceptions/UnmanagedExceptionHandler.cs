using System.Text.Json;
using CodeBlock.DevKit.Application.Notifications;
using CodeBlock.DevKit.Infrastructure.Resources;
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
    private readonly IStringLocalizer<InfrastructureResource> _localizer;
    #endregion

    #region Ctors


    public UnmanagedExceptionHandler(
        INotificationService notifications,
        ILogger<UnmanagedExceptionHandler<TRequest, TResponse, TException>> logger,
        IStringLocalizer<InfrastructureResource> localizer
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
        _notifications.Add(exception.GetType().Name, _localizer[InfrastructureResource.Unknown_Exception_Error]);

        _logger.LogError(exception, $"request : {JsonSerializer.Serialize(request)}");

        state.SetHandled(default);

        return Task.CompletedTask;
    }

    #endregion
}
