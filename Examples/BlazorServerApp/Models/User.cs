namespace BlazorServerApp.Models;

public class User
{
    public User(string userName)
    {
        UserName = userName;
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; private set; }

    public string UserName { get; private set; }
}
