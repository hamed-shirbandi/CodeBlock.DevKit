namespace CodeBlock.DevKit.Web.Api.JwtAuthentication;

public class JwtAuthenticationOptions
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public int ExpireDays { get; set; }
}
