using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Authorization.UI.Services;
using CodeBlock.DevKit.Authorization.UseCases.Users.GetUserById;
using CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUser;
using CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;
using CodeBlock.DevKit.Web.Blazor.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Authorization.UI.Pages.Account;

[AllowAnonymous]
public class RegisterModel : BasePageModel
{
    private readonly CookieAuthenticationService _cookieAuthenticationService;
    private readonly AuthenticationStateService _authenticationStateService;

    public RegisterModel(
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

        var getUserResult = await _inMemoryBus.SendQuery(new GetUserByIdRequest(registerUserResult.Value.EntityId));

        await _cookieAuthenticationService.SignInAsync(
            getUserResult.Value.Id,
            getUserResult.Value.Email,
            getUserResult.Value.Roles,
            isPersistent: true
        );

        _authenticationStateService.AddUserId(registerUserResult.Value.EntityId);

        return LocalRedirect(returnUrl ?? Url.Content("~/"));
    }
}
