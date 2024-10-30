using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Authorization.UseCases.Users.GetUserById;
using CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUser;
using CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;
using CodeBlock.DevKit.Web.Blazor.Server.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Authorization.UI.Pages.Account;

[AllowAnonymous]
public class RegisterModel : BasePageModel
{
    private readonly CookieAuthenticationService _cookieAuthenticationService;

    public RegisterModel(CookieAuthenticationService cookieAuthenticationService, IBus bus)
        : base(bus)
    {
        _cookieAuthenticationService = cookieAuthenticationService;
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

        var registerUserResult = await _bus.SendCommand(RegisterUserRequest);

        if (!registerUserResult.IsSuccess)
        {
            ParseResultToViewData(registerUserResult);
            return Page();
        }

        var getUserResult = await _bus.SendQuery(new GetUserByIdRequest(registerUserResult.Value.EntityId));

        await _cookieAuthenticationService.SignInAsync(
            getUserResult.Value.Id,
            getUserResult.Value.Email,
            getUserResult.Value.Roles,
            isPersistent: true
        );

        return LocalRedirect(returnUrl ?? Url.Content("~/"));
    }
}
