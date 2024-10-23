using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.Logging;

namespace CodeBlock.DevKit.Web.Blazor.Server.Services;

public class AuthenticationStateValidator : RevalidatingServerAuthenticationStateProvider
{
    protected override TimeSpan RevalidationInterval => TimeSpan.FromSeconds(10);
    private readonly AuthenticationStateService _authenticationStateService;

    public AuthenticationStateValidator(ILoggerFactory loggerFactory, AuthenticationStateService loginState)
        : base(loggerFactory)
    {
        _authenticationStateService = loginState;
    }

    protected override Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        var currentUserId = authenticationState.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Task.FromResult(_authenticationStateService.IsUserLoggedIn(currentUserId));
    }
}
