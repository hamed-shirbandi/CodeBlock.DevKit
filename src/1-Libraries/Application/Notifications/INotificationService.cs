namespace CodeBlock.DevKit.Application.Notifications;

public interface INotificationService
{
    void Add(string key, string value);
    void AddRange(List<Notification> notifications);
    List<string> GetErrors();
    List<Notification> GetList();
    List<Notification> GetListAndReset();
    bool HasAny();
    void Reset();
}
