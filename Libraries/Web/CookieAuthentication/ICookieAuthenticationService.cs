namespace CodeBlock.DevKit.Web.CookieAuthentication;

public interface ICookieAuthenticationService
{
    Task SignInAsync(string userId, string userName, bool isPersistent);
    Task SignOutAsync();
}
