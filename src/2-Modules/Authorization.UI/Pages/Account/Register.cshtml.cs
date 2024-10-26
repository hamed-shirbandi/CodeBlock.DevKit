using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUser;
using CodeBlock.DevKit.Web.Blazor.Server.Models;
using CodeBlock.DevKit.Web.Blazor.Server.Services;
using CodeBlock.DevKit.Web.CookieAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Authorization.UI.Pages.Account;

[AllowAnonymous]
public class RegisterModel : BasePageModel
{
    private readonly ICookieAuthenticationService _cookieAuthenticationService;
    private readonly AuthenticationStateService _authenticationStateService;

    public RegisterModel(
        ICookieAuthenticationService cookieAuthenticationService,
        IInMemoryBus inMemoryBus,
        AuthenticationStateService authenticationStateService
    )
        : base(inMemoryBus)
    {
        _cookieAuthenticationService = cookieAuthenticationService;
        _authenticationStateService = authenticationStateService;
    }

    [BindProperty]
    public RegisterUserRequest RegisterUserRequest { get; set; }

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

        var registerUserResult = await _inMemoryBus.SendCommand(RegisterUserRequest);

        if (!registerUserResult.IsSuccess)
        {
            ParseResultToViewData(registerUserResult);
            return Page();
        }

        await _cookieAuthenticationService.SignInAsync(registerUserResult.Value.EntityId, RegisterUserRequest.Email, isPersistent: true);

        _authenticationStateService.AddUserId(registerUserResult.Value.EntityId);

        return LocalRedirect(returnUrl ?? Url.Content("~/"));
    }
}
