using System.Text.Json;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Exceptions;
using MediatR.Pipeline;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace CodeBlock.DevKit.Infrastructure.Exceptions;

/// <summary>
/// Handle all managed exceptions (DomainException,ApplicationException,ValidationException)
/// </summary>
public class ManagedExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TException : ManagedException
{
    private readonly INotificationService _notifications;
    private readonly ILogger<ManagedExceptionHandler<TRequest, TResponse, TException>> _logger;
    private readonly IStringLocalizerFactory _localizerFactory;

    public ManagedExceptionHandler(
        INotificationService notifications,
        ILogger<ManagedExceptionHandler<TRequest, TResponse, TException>> logger,
        IStringLocalizerFactory localizerFactory
    )
    {
        _notifications = notifications;
        _logger = logger;
        _localizerFactory = localizerFactory;
    }

    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        var message = exception.Message;

        if (exception.HasResourceMessage())
            message = GetLocalizedMessage(exception);

        _notifications.Add(message);

        _logger.LogDebug(exception, message: $"request : {JsonSerializer.Serialize(request)}");

        state.SetHandled(default);

        return Task.CompletedTask;
    }

    private string GetLocalizedMessage(TException exception)
    {
        // Retrieve the main message using the main resource key and type
        var localizer = _localizerFactory.Create(exception.MessageResourceType);
        var mainMessage = localizer[exception.MessageResourceKey];

        // Fetch each placeholder resource
        var placeholders = exception
            .MessagePlaceholders.Select(mph =>
            {
                if (mph.Type == MessagePlaceholderType.Resource)
                {
                    var placeholderLocalizer = _localizerFactory.Create(mph.ResourceType);
                    return placeholderLocalizer[mph.ResourceKey];
                }
                else
                {
                    return mph.PlainText;
                }
            })
            .ToArray();

        // Format the main message with placeholders
        return string.Format(mainMessage, placeholders);
    }
}
