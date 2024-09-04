using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CodeBlock.DevKit.Web.CookieAuthentication;

public class CookieAuthenticationService : ICookieAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CookieAuthenticationOptions _options;

    public CookieAuthenticationService(IHttpContextAccessor httpContextAccessor, IOptions<CookieAuthenticationOptions> options)
    {
        _options = options != null ? options.Value : throw new ArgumentNullException(nameof(options));
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task SignInAsync(string userId, string userName, bool isPersistent)
    {
        var claims = GetClaims(userId, userName);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = _options.AllowRefresh,
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

    public List<Claim> GetClaims(string userId, string userName)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.NameIdentifier, userId) };

        return claims;
    }
}
