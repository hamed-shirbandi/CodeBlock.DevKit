// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeBlock.DevKit.Web.Blazor.Server.Pages;

public class BasePageModel : PageModel
{
    protected readonly IRequestDispatcher _requestDispatcher;

    protected BasePageModel(IRequestDispatcher requestDispatcher)
    {
        _requestDispatcher = requestDispatcher;
    }

    protected void ParseResultToViewData<T>(Result<T> result)
    {
        ViewData["IsSuccess"] = result.IsSuccess;
        ViewData["Message"] = result.Message;
        ViewData["Errors"] = result.Errors;
    }
}
