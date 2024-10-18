using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Authorization.UseCases.VerifyUserPassword;
using CodeBlock.DevKit.Web.CookieAuthentication;
using CodeBlock.DevKit.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Authorization.UI.Pages;

[AllowAnonymous]
public class LoginModel : BasePageModel
{
    private readonly ICookieAuthenticationService _cookieAuthenticationService;

    public LoginModel(ICookieAuthenticationService cookieAuthenticationService, IInMemoryBus inMemoryBus)
        : base(inMemoryBus)
    {
        _cookieAuthenticationService = cookieAuthenticationService;
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
            verifyUserPasswordResult.Value.UserName,
            isPersistent: true
        );

        return LocalRedirect(returnUrl ?? Url.Content("~/"));
    }
}
