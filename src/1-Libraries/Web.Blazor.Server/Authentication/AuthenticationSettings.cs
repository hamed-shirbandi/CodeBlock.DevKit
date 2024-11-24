namespace CodeBlock.DevKit.Web.Blazor.Server.Authentication;

public class AuthenticationSettings
{
    public AuthenticationSettings()
    {
        Settings = new();
        Cookie = new();
        Google = new();
        Twitter = new();
        Microsoft = new();
    }

    public AuthenticationControlSettings Settings { get; set; }
    public CookieAuthenticationSettings Cookie { get; set; }
    public GoogleAuthenticationSettings Google { get; set; }
    public TwitterAuthenticationSettings Twitter { get; set; }
    public MicrosoftAuthenticationSettings Microsoft { get; set; }
    public FacebookAuthenticationSettings Facebook { get; set; }

    public bool AnyExternalLoginProvider()
    {
        return Google.Enabled || Twitter.Enabled || Microsoft.Enabled || Facebook.Enabled;
    }
}

public class AuthenticationControlSettings
{
    public AuthenticationControlSettings()
    {
        EnableLogin = true;
        EnableRegister = true;
        ShowLogo = false;
        ShowAppName = false;
    }

    public bool EnableLogin { get; set; }
    public bool EnableRegister { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowAppName { get; set; }
}

public class CookieAuthenticationSettings
{
    public CookieAuthenticationSettings()
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

public class GoogleAuthenticationSettings
{
    public GoogleAuthenticationSettings()
    {
        Enabled = false;
        CallbackPath = "/signin-google";
    }

    public bool Enabled { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string CallbackPath { get; set; }
}

public class TwitterAuthenticationSettings
{
    public TwitterAuthenticationSettings()
    {
        Enabled = false;
        CallbackPath = "/signin-twitter";
    }

    public bool Enabled { get; set; }
    public string ConsumerKey { get; set; }
    public string ConsumerSecret { get; set; }
    public string CallbackPath { get; set; }
}

public class MicrosoftAuthenticationSettings
{
    public MicrosoftAuthenticationSettings()
    {
        Enabled = false;
        CallbackPath = "/signin-microsoft";
    }

    public bool Enabled { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string CallbackPath { get; set; }
}

public class FacebookAuthenticationSettings
{
    public FacebookAuthenticationSettings()
    {
        Enabled = false;
        CallbackPath = "/signin-facebook";
    }

    public bool Enabled { get; set; }
    public string AppId { get; set; }
    public string AppSecret { get; set; }
    public string CallbackPath { get; set; }
}
