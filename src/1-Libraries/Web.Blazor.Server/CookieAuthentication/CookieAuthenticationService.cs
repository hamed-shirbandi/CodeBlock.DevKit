using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;

public class CookieAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CookieAuthenticationSettings _settings;

    public CookieAuthenticationService(IHttpContextAccessor httpContextAccessor, CookieAuthenticationSettings settings)
    {
        _settings = settings;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task SignInAsync(string userId, string userName, IEnumerable<string> roles, bool isPersistent)
    {
        var claims = GetClaims(userId, userName, roles);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = _settings.AllowRefresh,
            IsPersistent = isPersistent,
            IssuedUtc = DateTime.UtcNow,
        };

        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );
    }

    public async Task SignOutAsync()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public List<Claim> GetClaims(string userId, string userName, IEnumerable<string> roles)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.NameIdentifier, userId) };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        return claims;
    }
}
