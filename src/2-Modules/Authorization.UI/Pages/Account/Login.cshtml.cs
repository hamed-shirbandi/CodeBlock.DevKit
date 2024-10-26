using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Authorization.UI.Services;
using CodeBlock.DevKit.Authorization.UseCases.Users.VerifyUserPassword;
using CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;
using CodeBlock.DevKit.Web.Blazor.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Authorization.UI.Pages.Account;

[AllowAnonymous]
public class LoginModel : BasePageModel
{
    private readonly CookieAuthenticationService _cookieAuthenticationService;
    private readonly AuthenticationStateService _authenticationStateService;

    public LoginModel(
        CookieAuthenticationService cookieAuthenticationService,
        IInMemoryBus inMemoryBus,
        AuthenticationStateService authenticationStateService
    )
        : base(inMemoryBus)
    {
        _cookieAuthenticationService = cookieAuthenticationService;
        _authenticationStateService = authenticationStateService;
    }

    [BindProperty]
    public VerifyUserPasswordRequest VerifyUserPasswordRequest { get; set; }

    /// <summary>
    ///
    /// </summary>
    public async Task OnGetAsync() { }

    /// <summary>
    ///
    /// </summary>
    public async Task<IActionResult> OnPostAsync([FromQuery] string returnUrl)
    {
        if (!ModelState.IsValid)
            return Page();

        var verifyUserPasswordResult = await _inMemoryBus.SendQuery(VerifyUserPasswordRequest);

        if (!verifyUserPasswordResult.IsSuccess)
        {
            ParseResultToViewData(verifyUserPasswordResult);
            return Page();
        }

        await _cookieAuthenticationService.SignInAsync(
            verifyUserPasswordResult.Value.Id,
            verifyUserPasswordResult.Value.Email,
            verifyUserPasswordResult.Value.Roles,
            isPersistent: true
        );

        _authenticationStateService.AddUserId(verifyUserPasswordResult.Value.Id);

        return LocalRedirect(returnUrl ?? Url.Content("~/"));
    }
}
