namespace CodeBlock.DevKit.Authorization.UI.Services;

public class AuthenticationStateService
{
    private readonly List<string> _loggedInUsers;

    public AuthenticationStateService()
    {
        _loggedInUsers = new();
    }

    public void AddUserId(string userId)
    {
        if (!IsUserLoggedIn(userId))
            _loggedInUsers.Add(userId);
    }

    public void RemoveUserId(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return;

        if (IsUserLoggedIn(userId))
            _loggedInUsers.Remove(userId);
    }

    public bool IsUserLoggedIn(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return false;

        return _loggedInUsers.Contains(userId);
    }
}
