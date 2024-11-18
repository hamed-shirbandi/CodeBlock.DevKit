namespace CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;

public class CookieAuthenticationSettings
{
    public CookieAuthenticationSettings()
    {
        Google = new();
    }

    public string CookieName { get; set; }
    public bool CookieHttpOnly { get; set; }
    public string LoginPath { get; set; }
    public string LogoutPath { get; set; }
    public int ExpireFromMinute { get; set; }
    public bool SlidingExpiration { get; set; }
    public bool AllowRefresh { get; set; }
    public GoogleAuthentication Google { get; set; }
}

public class GoogleAuthentication
{
    public GoogleAuthentication()
    {
        Enabled = false;
        CallbackPath = "/signin-google";
    }

    public bool Enabled { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string CallbackPath { get; set; }
}
