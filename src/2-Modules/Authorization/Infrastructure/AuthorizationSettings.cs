namespace CodeBlock.DevKit.Authorization.Infrastructure;

public class AuthorizationSettings
{
    public string AdminRole { get; set; }
    public string DefaultRole { get; set; }
    public AdminUserSettings AdminUser { get; set; }
}

public class AdminUserSettings
{
    public string Email { get; set; }
    public string Password { get; set; }
}
