// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.UseCases.Users.LoginUser;
using CodeBlock.DevKit.Web.Blazor.Server.Authentication;
using CodeBlock.DevKit.Web.Blazor.Server.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Authorization.UI.Pages.Account;

[AllowAnonymous]
public class LoginModel : BasePageModel
{
    private readonly CookieAuthenticationService _cookieAuthenticationService;
    private readonly AuthenticationSettings _authenticationSettings;

    public LoginModel(
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
    public LoginUserRequest VerifyUserPasswordRequest { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!_authenticationSettings.Settings.EnableLogin)
            return LocalRedirect("/non-existent-page");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync([FromQuery] string returnUrl)
    {
        if (!ModelState.IsValid)
            return Page();

        var verifyUserPasswordResult = await _requestDispatcher.SendQuery(VerifyUserPasswordRequest);

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
