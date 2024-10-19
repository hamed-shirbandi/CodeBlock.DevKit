using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Core.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeBlock.DevKit.Web.Blazor.Server.Models;

public class BasePageModel : PageModel
{
    protected readonly IInMemoryBus _inMemoryBus;

    protected BasePageModel(IInMemoryBus inMemoryBus)
    {
        _inMemoryBus = inMemoryBus;
    }

    protected void ParseResultToViewData<T>(Result<T> result)
    {
        ViewData["Message"] = result.Message;
        ViewData["Errors"] = result.Errors;
    }
}
