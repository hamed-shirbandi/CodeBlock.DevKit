using System.Text.Json;
using CodeBlock.DevKit.Application.Notifications;
using CodeBlock.DevKit.Core.Exceptions;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CodeBlock.DevKit.Infrastructure.Exceptions;

/// <summary>
/// Handle all managed exceptions (DomainException,ApplicationException,ValidationException)
/// </summary>
public class ManagedExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TException : ManagedException
{
    #region Fields


    private readonly INotificationService _notifications;
    private readonly ILogger<ManagedExceptionHandler<TRequest, TResponse, TException>> _logger;

    #endregion

    #region Ctors


    public ManagedExceptionHandler(INotificationService notifications, ILogger<ManagedExceptionHandler<TRequest, TResponse, TException>> logger)
    {
        _notifications = notifications;
        _logger = logger;
    }

    #endregion

    #region Handler



    /// <summary>
    ///
    /// </summary>
    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        //notify exception message if any
        if (!string.IsNullOrEmpty(exception.Message))
            _notifications.Add(exceptionType.Name, exception.Message);

        _logger.LogWarning(exception, message: $"request : {JsonSerializer.Serialize(request)}");

        state.SetHandled(default);

        return Task.CompletedTask;
    }

    #endregion
}
