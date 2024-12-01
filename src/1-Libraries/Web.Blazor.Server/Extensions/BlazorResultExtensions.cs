using Blazored.Modal;
using Blazored.Toast.Services;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Blazor.Server.Services;
using Microsoft.AspNetCore.Components;

namespace CodeBlock.DevKit.Web.Blazor.Server.Extensions;

public static class BlazorResultExtensions
{
    public static Result<T> ShowToast<T>(this Result<T> result, IToastService toastService)
    {
        if (result.IsSuccess)
        {
            if (typeof(T) == typeof(CommandResult))
            {
                var commandResult = result.Value as CommandResult;
                toastService.ShowSuccess(commandResult.Message, result.Message);
            }
            else
            {
                toastService.ShowSuccess(result.Message, "");
            }
        }
        else
        {
            toastService.ShowError(result.Errors.ParseToFragment(), result.Message);
        }

        return result;
    }

    public static Result<T> ShowErrorToast<T>(this Result<T> result, IToastService toastService)
    {
        if (!result.IsSuccess)
            toastService.ShowError(result.Errors.ParseToFragment(), result.Message);

        return result;
    }

    public static Result<T> CloseModal<T>(this Result<T> result, BlazoredModalInstance modalInstance)
    {
        if (result.IsSuccess)
            modalInstance.CloseAsync();

        return result;
    }

    public static Result<T> NavigateTo<T>(this Result<T> result, string url, NavigationManager navigationManager)
    {
        if (result.IsSuccess)
            navigationManager.NavigateTo(url);

        return result;
    }

    public static Result<T> PublishMessage<T>(
        this Result<T> result,
        MessageService messageService,
        string onSuccessResultMessageKey = "",
        string onFailedResultMessageKey = ""
    )
    {
        if (string.IsNullOrEmpty(onSuccessResultMessageKey) && string.IsNullOrEmpty(onFailedResultMessageKey))
            return result;

        if (result.IsSuccess)
            messageService.SendMessage(onSuccessResultMessageKey);
        else
            messageService.SendMessage(onFailedResultMessageKey);

        return result;
    }

    private static RenderFragment ParseToFragment(this List<string> errors)
    {
        RenderFragment content = null;

        if (errors.Count == 0)
            return content;

        foreach (var error in errors)
            content += AddMarkupContent($"<text><strong>-</strong> {error} <br/></text>");

        return content;
    }

    private static RenderFragment AddMarkupContent(string txt) =>
        builder =>
        {
            builder.AddMarkupContent(1, txt);
        };
}
