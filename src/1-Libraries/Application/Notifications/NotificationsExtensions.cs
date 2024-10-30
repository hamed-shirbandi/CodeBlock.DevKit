using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Application.Notifications;

public static class NotificationsExtensions
{
    public static void AddNotificationService(this IServiceCollection services)
    {
        services.AddScoped<NotificationService>();
    }
}
