// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Queries;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.IsEmailRegistered;

public class IsEmailRegisteredRequest : BaseQuery<bool>
{
    public IsEmailRegisteredRequest(string email)
    {
        Email = email;
    }

    public string Email { get; set; }
}
