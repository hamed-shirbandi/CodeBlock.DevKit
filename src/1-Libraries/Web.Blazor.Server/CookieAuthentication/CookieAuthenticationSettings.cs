namespace CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;

public class CookieAuthenticationSettings
{
    public CookieAuthenticationSettings()
    {
        Cookie = new();
        Google = new();
        Twitter = new();
        Microsoft = new();
    }

    public CookieAuthentication Cookie { get; set; }
    public GoogleAuthentication Google { get; set; }
    public TwitterAuthentication Twitter { get; set; }
    public MicrosoftAuthentication Microsoft { get; set; }
}

public class CookieAuthentication
{
    public CookieAuthentication()
    {
        Enabled = false;
    }

    public bool Enabled { get; set; }
    public string CookieName { get; set; }
    public bool CookieHttpOnly { get; set; }
    public string LoginPath { get; set; }
    public string LogoutPath { get; set; }
    public int ExpireFromMinute { get; set; }
    public bool SlidingExpiration { get; set; }
    public bool AllowRefresh { get; set; }
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

public class TwitterAuthentication
{
    public TwitterAuthentication()
    {
        Enabled = false;
        CallbackPath = "/signin-twitter";
    }

    public bool Enabled { get; set; }
    public string ConsumerKey { get; set; }
    public string ConsumerSecret { get; set; }
    public string CallbackPath { get; set; }
}

public class MicrosoftAuthentication
{
    public MicrosoftAuthentication()
    {
        Enabled = false;
        CallbackPath = "/signin-microsoft";
    }

    public bool Enabled { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string CallbackPath { get; set; }
}
