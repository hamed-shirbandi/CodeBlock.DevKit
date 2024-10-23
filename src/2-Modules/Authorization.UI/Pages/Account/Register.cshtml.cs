using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Authorization.UseCases.RegisterUser;
using CodeBlock.DevKit.Web.Blazor.Server.Models;
using CodeBlock.DevKit.Web.CookieAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Authorization.UI.Pages.Account;

[AllowAnonymous]
public class RegisterModel : BasePageModel
{
    private readonly ICookieAuthenticationService _cookieAuthenticationService;

    public RegisterModel(ICookieAuthenticationService cookieAuthenticationService, IInMemoryBus inMemoryBus)
        : base(inMemoryBus)
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
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var registerUserResult = await _inMemoryBus.SendCommand(RegisterUserRequest);
        if (registerUserResult.IsSuccess)
        {
            await _cookieAuthenticationService.SignInAsync(
                registerUserResult.Value.EntityId,
                RegisterUserRequest.Email ?? RegisterUserRequest.Mobile,
                isPersistent: true
            );
        }

        ParseResultToViewData(registerUserResult);
        return Page();
    }
}
