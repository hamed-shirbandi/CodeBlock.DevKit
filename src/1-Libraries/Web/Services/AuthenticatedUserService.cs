using System.Security.Claims;
using CodeBlock.DevKit.Application.Srvices;
using Microsoft.AspNetCore.Http;

namespace CodeBlock.DevKit.Web.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId()
    {
        var user = _httpContextAccessor.HttpContext.User;
        if (user == null)
            return "";
        return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
    }

    public string GetUserName()
    {
        var user = _httpContextAccessor.HttpContext.User;
        if (user == null)
            return "";
        return user.FindFirstValue(ClaimTypes.Name) ?? "";
    }

    public string GetEmail()
    {
        var user = _httpContextAccessor.HttpContext.User;
        if (user == null)
            return "";
        return user.FindFirstValue(ClaimTypes.Email) ?? "";
    }

    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
