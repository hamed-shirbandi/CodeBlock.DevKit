using CodeBlock.DevKit.Application.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Infrastructure.Notifications;

public static class NotificationsExtensions
{
    /// <summary>
    ///
    /// </summary>
    public static void AddNotificationService(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();
    }
}
