using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Authorization.UseCases.Users.LoginUser;
using CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;
using CodeBlock.DevKit.Web.Blazor.Server.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Authorization.UI.Pages.Account;

[AllowAnonymous]
public class LoginModel : BasePageModel
{
    private readonly CookieAuthenticationService _cookieAuthenticationService;

    public LoginModel(CookieAuthenticationService cookieAuthenticationService, IInMemoryBus inMemoryBus)
        : base(inMemoryBus)
    {
        _cookieAuthenticationService = cookieAuthenticationService;
    }

    [BindProperty]
    public LoginUserRequest VerifyUserPasswordRequest { get; set; }

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

        return LocalRedirect(returnUrl ?? Url.Content("~/"));
    }
}
