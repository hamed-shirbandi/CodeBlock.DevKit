namespace CodeBlock.DevKit.Authorization.Infrastructure;

public class AuthorizationOptions
{
    public string AdminRole { get; set; }
    public string DefaultRole { get; set; }
    public AdminUserOptions AdminUser { get; set; }
}

public class AdminUserOptions
{
    public string Email { get; set; }
    public string Password { get; set; }
}
