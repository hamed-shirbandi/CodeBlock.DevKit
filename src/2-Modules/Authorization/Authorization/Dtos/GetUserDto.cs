// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Authorization.Resources;

namespace CodeBlock.DevKit.Authorization.Dtos;

public class GetUserDto
{
    public string Id { get; set; }

    [Display(Name = nameof(AuthorizationResource.User_Email), ResourceType = typeof(AuthorizationResource))]
    public string Email { get; set; }

    [Display(Name = nameof(AuthorizationResource.User_Roles), ResourceType = typeof(AuthorizationResource))]
    public IEnumerable<string> Roles { get; set; }
}

