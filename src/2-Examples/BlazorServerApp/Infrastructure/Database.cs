using BlazorServerApp.Models;

namespace BlazorServerApp.Infrastructure;

public class Database
{
    public Database()
    {
        Users = new List<User>();
        SeedAdminUser();
    }

    public List<User> Users { get; private set; }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    private void SeedAdminUser()
    {
        var user = new User("admin");
        AddUser(user);
    }
}
