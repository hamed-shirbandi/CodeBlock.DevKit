using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Core.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeBlock.DevKit.Web.Blazor.Server.Pages;

public class BasePageModel : PageModel
{
    protected readonly IBus _bus;

    protected BasePageModel(IBus bus)
    {
        _bus = bus;
    }

    protected void ParseResultToViewData<T>(Result<T> result)
    {
        ViewData["IsSuccess"] = result.IsSuccess;
        ViewData["Message"] = result.Message;
        ViewData["Errors"] = result.Errors;
    }
}
