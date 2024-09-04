using CodeBlock.DevKit.Application.Notifications;

namespace CodeBlock.DevKit.Infrastructure.Notifications;

/// <summary>
///
/// </summary>
public class NotificationService : INotificationService
{
    private List<Notification> notifications;

    public NotificationService()
    {
        notifications = new List<Notification>();
    }

    public void Add(string key, string value)
    {
        var notification = new Notification(key, value);
        notifications.Add(notification);
    }

    public void AddRange(List<Notification> notifications)
    {
        this.notifications.AddRange(notifications);
    }

    public List<string> GetErrors()
    {
        return notifications.Select(n => n.Value).ToList();
    }

    public List<Notification> GetList()
    {
        return notifications;
    }

    public List<Notification> GetListAndReset()
    {
        var notifications = this.notifications;
        Reset();
        return notifications;
    }

    public bool HasAny()
    {
        return notifications.Count != 0;
    }

    public void Reset()
    {
        notifications = new List<Notification>();
    }
}
