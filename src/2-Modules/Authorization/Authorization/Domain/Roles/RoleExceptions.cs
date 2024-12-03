// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Exceptions;

namespace CodeBlock.DevKit.Authorization.Domain.Roles;

internal static class RoleExceptions
{
    public static DomainException NameIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(AuthorizationResource.Role_Name, typeof(AuthorizationResource)) }
        );
    }

    public static DomainException NameMustBeUnique()
    {
        return new DomainException(
            nameof(CoreResource.ALready_Exists),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(AuthorizationResource.Role_Name, typeof(AuthorizationResource)) }
        );
    }
}
