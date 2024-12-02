// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;
using CodeBlock.DevKit.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;

namespace CodeBlock.DevKit.Web.Api.Filters;

public class ModelStateValidatingAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            List<string> errors = GetErrors(context.ModelState);

            Log.Logger.Information($"Incoming model is not valid. Errors => {JsonSerializer.Serialize(errors)}");

            var result = Result.Failure(errors);

            context.Result = new ObjectResult(context.ModelState) { Value = result, StatusCode = StatusCodes.Status400BadRequest };
        }

        base.OnActionExecuting(context);
    }

    public static List<string> GetErrors(ModelStateDictionary modelState)
    {
        var errors = new List<string>();
        foreach (var value in modelState.Values)
        {
            foreach (var error in value.Errors)
            {
                errors.Add(error.ErrorMessage);
            }
        }
        return errors;
    }
}

