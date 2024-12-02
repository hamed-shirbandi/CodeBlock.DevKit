// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Authorization.Dtos;

namespace CodeBlock.DevKit.Authorization.Api.Models;

public class LoginUserResponse
{
    public string Token { get; set; }
    public GetUserDto User { get; set; }
}

