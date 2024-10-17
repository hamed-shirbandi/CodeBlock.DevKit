using Blazored.Modal;
using Blazored.Toast.Services;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Helpers;
using Microsoft.AspNetCore.Components;

namespace CodeBlock.DevKit.Web.Extensions;

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
}
