namespace CodeBlock.DevKit.Application.Srvices;

public interface INotificationService
{
    void Add(string notification);
    void AddRange(List<string> notifications);
    List<string> GetList();
    List<string> GetListAndReset();
    bool HasAny();
    void Reset();
}
