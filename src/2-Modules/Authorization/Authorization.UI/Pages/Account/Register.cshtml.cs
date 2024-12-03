// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.UseCases.Users.GetUserById;
using CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUser;
using CodeBlock.DevKit.Web.Blazor.Server.Authentication;
using CodeBlock.DevKit.Web.Blazor.Server.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Authorization.UI.Pages.Account;

[AllowAnonymous]
public class RegisterModel : BasePageModel
{
    private readonly CookieAuthenticationService _cookieAuthenticationService;
    private readonly AuthenticationSettings _authenticationSettings;

    public RegisterModel(
        CookieAuthenticationService cookieAuthenticationService,
        IRequestDispatcher requestDispatcher,
        AuthenticationSettings authenticationSettings
    )
        : base(requestDispatcher)
    {
        _cookieAuthenticationService = cookieAuthenticationService;
        _authenticationSettings = authenticationSettings;
    }

    [BindProperty]
    public RegisterUserRequest RegisterUserRequest { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!_authenticationSettings.Settings.EnableRegister)
            return LocalRedirect("/non-existent-page");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync([FromQuery] string returnUrl)
    {
        if (!ModelState.IsValid)
            return Page();

        var registerUserResult = await _requestDispatcher.SendCommand(RegisterUserRequest);

        if (!registerUserResult.IsSuccess)
        {
            ParseResultToViewData(registerUserResult);
            return Page();
        }

        var getUserResult = await _requestDispatcher.SendQuery(new GetUserByIdRequest(registerUserResult.Value.EntityId));

        await _cookieAuthenticationService.SignInAsync(
            getUserResult.Value.Id,
            getUserResult.Value.Email,
            getUserResult.Value.Roles,
            isPersistent: true
        );

        return LocalRedirect(returnUrl ?? Url.Content("~/"));
    }
}
