// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace CodeBlock.DevKit.Authorization.Exceptions;

internal static class AuthorizationExceptions
{
    public static ApplicationException RoleNotFound(string searchedKey)
    {
        return new ApplicationException(
            nameof(CoreResource.Record_Not_Found),
            typeof(CoreResource),
            new List<MessagePlaceholder>
            {
                MessagePlaceholder.CreateResource(AuthorizationResource.Role, typeof(AuthorizationResource)),
                MessagePlaceholder.CreatePlainText(searchedKey),
            }
        );
    }

    public static ApplicationException UserNotFound(string searchedKey)
    {
        return new ApplicationException(
            nameof(CoreResource.Record_Not_Found),
            typeof(CoreResource),
            new List<MessagePlaceholder>
            {
                MessagePlaceholder.CreateResource(AuthorizationResource.User, typeof(AuthorizationResource)),
                MessagePlaceholder.CreatePlainText(searchedKey),
            }
        );
    }

    internal static Exception InvalidPassword()
    {
        return new ApplicationException(nameof(AuthorizationResource.User_Password_Is_Wrong), typeof(AuthorizationResource));
    }
}
